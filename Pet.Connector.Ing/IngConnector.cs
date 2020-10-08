using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NBB.Application.DataContracts;
using OfficeOpenXml;
using Pet.Application.Commands.Banking;
using Pet.Connector.Abstractions;

namespace Pet.Connector.Ing
{
    public class IngConnector : IConnector
    {
        const int DATE_COLUMN = 1;
        const int TRANSACTION_DETAILS_COLUMN = 2;
        const int DEBIT_COLUMN = 3;
        const int CREDIT_COLUMN = 4;

        static List<Func<ExcelWorksheet, int, (bool, Command)>> Matchers = new List<Func<ExcelWorksheet, int, (bool, Command)>>()
        {
            MatchPosPayment,
            MatchBankTransfer,
            MatchDirectDebitPayment,
            MatchCashWithdrawal,
            MatchRoundUp,
            MatchExchange,
            MatchCollection
        };
        static Func<ExcelWorksheet, int, (bool, Command)> Match = Matchers.Aggregate(MPlus);

        //const 
        public IEnumerable<Command> GetCommandsFromBankReport(Stream xlsStream)
        {
            var xlsxStream = ConvertXls2Xlsx(xlsStream);
            var pck = new ExcelPackage();
            pck.Load(xlsxStream);
            var worksheet = pck.Workbook.Worksheets[0];
            var rowsCnt = worksheet.Dimension.Rows;

            var currentRowNo = 1;
            var headerIdentified = false;
            while (currentRowNo < rowsCnt && !headerIdentified)
            {
                if (MatchHeader(worksheet, currentRowNo))
                {
                    headerIdentified = true;
                }

                currentRowNo = currentRowNo + 1;
            }

            if (!headerIdentified)
            {
                throw new Exception("Could not identify report header");
            }

            while (currentRowNo < rowsCnt)
            {
                var (matched, cmd) = Match(worksheet, currentRowNo);
                if (matched)
                {
                    yield return cmd;
                }

                currentRowNo++;
            }

        }

        static Stream ConvertXls2Xlsx(Stream stream)
        {
            var wb = new Spire.Xls.Workbook();
            wb.LoadFromStream(stream);
            var outputStream = new MemoryStream();
            wb.SaveToStream(outputStream, Spire.Xls.FileFormat.Version2016);
            return outputStream;
        }

        static bool Check(object value, string expectedText)
        {
            var actualText = value as string;
            return actualText == expectedText;
        }

        static bool CheckStartsWith(object value, string expectedText)
        {
            var actualText = value as string;
            return actualText != null && actualText.StartsWith(expectedText);
        }

        static string[] SplitLines(string value)
        {
            if (value != null)
            {
                return value.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[] { };
        }

        static Func<ExcelWorksheet, int, (bool, Command)> MPlus(Func<ExcelWorksheet, int, (bool, Command)> first, Func<ExcelWorksheet, int, (bool, Command)> second) =>
            (ExcelWorksheet worksheet, int rowIndex) =>
            {
                var (matched, cmd) = first(worksheet, rowIndex);
                if (!matched)
                {
                    (matched, cmd) = second(worksheet, rowIndex);
                }
                return (matched, cmd);
            };

        static bool MatchHeader(ExcelWorksheet worksheet, int rowIndex)
        {
            return
                Check(worksheet.Cells[rowIndex, DATE_COLUMN].Value, "Data") &&
                Check(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Detalii tranzactie") &&
                Check(worksheet.Cells[rowIndex, DEBIT_COLUMN].Value, "Debit") &&
                Check(worksheet.Cells[rowIndex, CREDIT_COLUMN].Value, "Credit");
        }

        static (bool, Command) MatchPosPayment(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Cumparare POS"))
            {
                var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var details = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());
                var posTerminalCode = details[1].Replace("Terminal: ", "");
                var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();

                return (true, new AddPosPayment.Command(posTerminalCode, value, paymentDate));
            }

            return (false, null);
        }

        static (bool, Command) MatchBankTransfer(ExcelWorksheet worksheet, int rowIndex)
        {
            const string IN_CONTUL = "In contul: ";
            const string DETALII = "Detalii: ";
            const string TRANSFER_HOME_BANK_BENEFICIAR = "Transfer Home'BankBeneficiar: ";
            const string TRANSFER_HOME_BANK_REFERINTA = "Transfer Home'BankReferinta";
            const string BENEFICIAR = "Beneficiar: ";
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, TRANSFER_HOME_BANK_BENEFICIAR) ||
                CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, TRANSFER_HOME_BANK_REFERINTA))
            {
                string FindAndReplace(IEnumerable<string> source, string key) =>
                    source
                    .FirstOrDefault(x => x.StartsWith(key))
                    ?.Replace(key, "");

                var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());
                var recipientName = FindAndReplace(detailsColumn, TRANSFER_HOME_BANK_BENEFICIAR) ?? FindAndReplace(detailsColumn, BENEFICIAR);
                var iban = FindAndReplace(detailsColumn, IN_CONTUL);
                var details = FindAndReplace(detailsColumn, DETALII) ?? string.Empty;
                var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();

                return (true, new AddBankTransfer.Command(iban, recipientName, details, value, paymentDate));
            }
           

            return (false, null);
        }

        static (bool, Command) MatchDirectDebitPayment(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Plata debit direct"))
            {

                var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());
                var directDebitCode = detailsColumn[0].Replace("Plata debit directBeneficiar: ", "");
                var details = detailsColumn[3].Replace("Detalii: ", "");
                var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();

                return (true, new AddDirectDebitPayment.Command(directDebitCode, value, details, paymentDate));
            }

            return (false, null);
        }

        static (bool, Command) MatchCashWithdrawal(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Retragere numerar"))
            {
                var withdrawalDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());
                var cashTerminal = detailsColumn[1].Replace("Terminal: ", "");
                var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();

                return (true, new AddCashWithdrawal.Command(cashTerminal, value, withdrawalDate));
            }

            return (false, null);
        }

        static (bool, Command) MatchRoundUp(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Tranzactie Round Up"))
            {
                var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());
                var iban = detailsColumn[1].Replace("In contul: ", "");
                var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();

                return (true, new AddRoundUp.Command(iban, value, paymentDate));
            }

            return (false, null);
        }

        static (bool, Command) MatchExchange(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "Schimb valutar"))
            {
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());

                if (CheckStartsWith(detailsColumn[1], "In contul:"))
                {
                    var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                    var iban = detailsColumn[1].Replace("In contul: ", "");
                    var exchangeValue = detailsColumn[2].Replace("Suma: ", "");
                    var exchangeRate = detailsColumn[3].Replace("Rata: ", "");
                    var details = "OutGoing";
                    var value = worksheet.Cells[rowIndex, DEBIT_COLUMN].GetValue<decimal>();
                    return (true, new AddExchange.Command(iban, exchangeValue, exchangeRate, details, value, paymentDate));
                }
                else if (CheckStartsWith(detailsColumn[1], "Din contul:"))
                {
                    var paymentDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                    var iban = detailsColumn[1].Replace("Din contul: ", "");
                    var exchangeValue = detailsColumn[2].Replace("Suma: ", "");
                    var exchangeRate = detailsColumn[3].Replace("Rata: ", "");
                    var details = "InComming";
                    var value = -worksheet.Cells[rowIndex, CREDIT_COLUMN].GetValue<decimal>();
                    return (true, new AddExchange.Command(iban, exchangeValue, exchangeRate, details, value, paymentDate));
                }
            }

            return (false, null);
        }

        static (bool, Command) MatchCollection(ExcelWorksheet worksheet, int rowIndex)
        {
            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "IncasareOrdonator"))
            {
                var incomeDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var value = worksheet.Cells[rowIndex, CREDIT_COLUMN].GetValue<decimal>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());

                var from = detailsColumn[0].Replace("IncasareOrdonator: ", "");
                var fromIban = detailsColumn[1].Replace("Din contul: ", "");
                var details = detailsColumn[2].Replace("Detalii: ", "");

                return (true, new AddCollection.Command(from, fromIban, details, value, incomeDate));
            }

            if (CheckStartsWith(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].Value, "IncasareReferinta"))
            {
                var incomeDate = worksheet.Cells[rowIndex, DATE_COLUMN].GetValue<DateTime>();
                var value = worksheet.Cells[rowIndex, CREDIT_COLUMN].GetValue<decimal>();
                var detailsColumn = SplitLines(worksheet.Cells[rowIndex, TRANSACTION_DETAILS_COLUMN].GetValue<string>());

                var from = detailsColumn[1].Replace("Ordonator: ", "");
                var fromIban = detailsColumn[2].Replace("Din contul: ", "");
                var details = detailsColumn[3].Replace("Detalii: ", "");

                return (true, new AddCollection.Command(from, fromIban, details, value, incomeDate));
            }

            return (false, null);
        }
    }
}
