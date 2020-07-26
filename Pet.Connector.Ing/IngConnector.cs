using System;
using System.Collections.Generic;
using System.IO;
using NBB.Application.DataContracts;
using OfficeOpenXml;
using Pet.Application.Commands.Banking;
using Pet.Connector.Abstractions;

namespace Pet.Connector.Ing
{
    public class IngConnector : IConnector
    {
        private int DATE_COLUMN = 2;
        private int TRANSACTION_DETAILS_COLUMN = 8;
        private int DEBIT_COLUMN = 16;
        private int CREDIT_COLUMN = 17;
        private bool _headerIdentified = false;

        //const 
        public IEnumerable<Command> GetCommandsFromBankReport(Stream stream)
        {
            var pck = new ExcelPackage();
            pck.Load(stream);
            var worksheet = pck.Workbook.Worksheets[0];
            var rowsCnt = worksheet.Dimension.Rows;

            var currentRowNo = 1;
            while (currentRowNo < rowsCnt)
            {
                if (IsHeaderRow(worksheet, currentRowNo))
                {
                    currentRowNo = currentRowNo + 1;
                    _headerIdentified = true;
                    break;
                }
                else
                {
                    currentRowNo = currentRowNo + 1;
                }
            }

            if (!_headerIdentified)
            {
                throw new Exception("Could not identify report header");
            }

            while (currentRowNo < rowsCnt)
            {
                if (IsPosPaymentFirstRow(worksheet, currentRowNo))
                {
                    var command = GetPosPayment(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 4;
                }
                else if (IsBankTransferFirstRow(worksheet, currentRowNo))
                {
                    var command = GetBankTransfer(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 6;
                }
                else if(IsBankTransferBetweenMyAccountsFirstRow(worksheet, currentRowNo))
                {
                    var command = GetBankTransferBetweenMyAccounts(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 5;
                }
                else if (IsDirectDebitPaymentFirstRow(worksheet, currentRowNo))
                {
                    var command = GetDirectDebitPayment(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 7;
                }
                else if (IsCashWithdrawalFirstRow(worksheet, currentRowNo))
                {
                    var command = GetCashWithdrawal(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 4;
                }
                else if (IsRoundUpFirstRow(worksheet, currentRowNo))
                {
                    var command = GetRoundUp(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 3;
                }
                else if (IsExchangeFirstRow(worksheet, currentRowNo))
                {
                    var command = GetExchage(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 7;
                }
                else if (IsCollectionFirstRow(worksheet, currentRowNo))
                {
                    var command = GetCollection(worksheet, currentRowNo);
                    yield return command;
                    currentRowNo = currentRowNo + 5;
                }
                else
                {
                    currentRowNo = currentRowNo + 1;
                }
            }

        }

        private bool IsHeaderRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string dateColumnHeaderText = "Data";
            var dateColumnHeaderFound = false;

            const string transactionDetailsColumnHeaderText = "Detalii tranzactie";
            var transactionDetailsColumnHeaderFound = false;

            const string debitColumnHeaderText = "Debit";
            var debitColumnHeaderFound = false;

            var columnsCnt = worksheet.Dimension.Columns;
            var currentColNo = 1;
            while(currentColNo < columnsCnt)
            {
                var text = worksheet.Cells[rowNo, currentColNo].Value as string;
                if(!dateColumnHeaderFound && text == dateColumnHeaderText)
                {
                    DATE_COLUMN = currentColNo;
                    dateColumnHeaderFound = true;
                }
                if (!transactionDetailsColumnHeaderFound && text == transactionDetailsColumnHeaderText)
                {
                    TRANSACTION_DETAILS_COLUMN = currentColNo;
                    transactionDetailsColumnHeaderFound = true;
                }
                if (!debitColumnHeaderFound && text == debitColumnHeaderText)
                {
                    DEBIT_COLUMN = currentColNo;
                    debitColumnHeaderFound = true;
                }

                currentColNo++;
            }

            return dateColumnHeaderFound && transactionDetailsColumnHeaderFound && debitColumnHeaderFound;
        }

        private bool IsPosPaymentFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string posPaymentTransactionType = "Cumparare POS";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == posPaymentTransactionType;
        }

        private bool IsBankTransferFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string bankTransferTransactionType = "Transfer Home'Bank";
            const string bankTransferNextRowText = "Beneficiar: ";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;
            var nextRowText = worksheet.Cells[rowNo+1, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == bankTransferTransactionType && nextRowText.StartsWith(bankTransferNextRowText);
        }

        private bool IsBankTransferBetweenMyAccountsFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string bankTransferBetweenMyAccountsTransactionType = "Transfer Home'Bank";
            const string bankTransferBetweenMyAccountsNextRowText = "Referinta: ";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;
            var nextRowText = worksheet.Cells[rowNo + 1, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == bankTransferBetweenMyAccountsTransactionType && nextRowText.StartsWith(bankTransferBetweenMyAccountsNextRowText);
        }

        private bool IsDirectDebitPaymentFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string directDebitTransactionType = "Plata debit direct";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == directDebitTransactionType;
        }

        private bool IsCashWithdrawalFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string cashWithdrawalTransactionType = "Retragere numerar";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == cashWithdrawalTransactionType;
        }

        private bool IsRoundUpFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string roundUpTransactionType = "Tranzactie Round Up";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == roundUpTransactionType;
        }

        private bool IsExchangeFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string exchangeTransactionType = "Schimb valutar Home'Bank";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == exchangeTransactionType;
        }

        private bool IsCollectionFirstRow(ExcelWorksheet worksheet, int rowNo)
        {
            const string collectionTransactionType = "Incasare";
            var transactionType = worksheet.Cells[rowNo, TRANSACTION_DETAILS_COLUMN].Value as string;

            return transactionType == collectionTransactionType;
        }

        private AddPosPayment.Command GetPosPayment(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var posTerminalCode = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Terminal: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddPosPayment.Command(posTerminalCode, value, paymentDate);
        }

        private AddBankTransfer.Command GetBankTransfer(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var recipientName = worksheet.Cells[rowNo + 1, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Beneficiar: ", "");
            var iban = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("In contul: ", "");
            var details = worksheet.Cells[rowNo + 4, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Detalii: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddBankTransfer.Command(iban, recipientName, details, value, paymentDate);
        }

        private AddBankTransfer.Command GetBankTransferBetweenMyAccounts(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var recipientName = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Beneficiar: ", "");
            var iban = worksheet.Cells[rowNo + 3, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("In contul: ", "");
            var details = worksheet.Cells[rowNo + 4, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Detalii: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddBankTransfer.Command(iban, recipientName, details, value, paymentDate);
        }

        private AddDirectDebitPayment.Command GetDirectDebitPayment(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var directDebitCode = worksheet.Cells[rowNo + 1, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Beneficiar: ", "");
            var details = worksheet.Cells[rowNo + 4, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Detalii: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddDirectDebitPayment.Command(directDebitCode, value, details, paymentDate);
        }

        private AddCashWithdrawal.Command GetCashWithdrawal(ExcelWorksheet worksheet, int rowNo)
        {
            var withdrawalDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var cashTerminal = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Terminal: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddCashWithdrawal.Command(cashTerminal, value, withdrawalDate);
        }

        private AddRoundUp.Command GetRoundUp(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var iban = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("In contul: ", "");
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddRoundUp.Command(iban, value, paymentDate);
        }

        private AddExchange.Command GetExchage(ExcelWorksheet worksheet, int rowNo)
        {
            var paymentDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var iban = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("In contul: ", "");
            var exchangeValue = worksheet.Cells[rowNo + 3, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Suma: ", "");
            var exchangeRate = worksheet.Cells[rowNo + 4, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Rata: ", "");
            var details = worksheet.Cells[rowNo + 6, TRANSACTION_DETAILS_COLUMN].GetValue<string>();
            var value = worksheet.Cells[rowNo, DEBIT_COLUMN].GetValue<decimal>();

            return new AddExchange.Command(iban, exchangeValue, exchangeRate, details, value, paymentDate);
        }

        private AddCollection.Command GetCollection(ExcelWorksheet worksheet, int rowNo)
        {
            var incomeDate = worksheet.Cells[rowNo, DATE_COLUMN].GetValue<DateTime>();
            var value = worksheet.Cells[rowNo, CREDIT_COLUMN].GetValue<decimal>();
            var nextDetailsRow = worksheet.Cells[rowNo + 1, TRANSACTION_DETAILS_COLUMN].GetValue<string>();
            if (nextDetailsRow.StartsWith("Referinta: "))
            {
                rowNo++; //skip one
            }

            var from = worksheet.Cells[rowNo + 1, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Ordonator: ", "");
            var fromIban = worksheet.Cells[rowNo + 2, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Din contul: ", "");
            var details = worksheet.Cells[rowNo + 3, TRANSACTION_DETAILS_COLUMN].GetValue<string>().Replace("Detalii: ", "");

            return new AddCollection.Command(from, fromIban, details, value, incomeDate);
        }
    }
}
