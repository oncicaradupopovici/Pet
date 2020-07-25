using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pet.Banking.Domain.BankTransferAggregate.DomainEvents;
using Pet.Banking.Domain.RoundUpAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class SavingsService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly SavingsTransactionFactory _savingsTransactionFactory;
        private readonly IExpenseRepository _expenseRepository;

        public SavingsService(ISavingsAccountRepository savingsAccountRepository, SavingsTransactionFactory savingsTransactionFactory, IExpenseRepository expenseRepository)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _savingsTransactionFactory = savingsTransactionFactory;
            _expenseRepository = expenseRepository;
        }

        public async Task<SavingsTransaction> CreateSavingsTransactionWhen(BankTransferAdded notification)
        {
            var isSavingsAccount =
                await _savingsAccountRepository.IsSavingsAccount(notification.Iban);
            if (isSavingsAccount)
            {
                var savingsTransaction = _savingsTransactionFactory.CreateFrom(SavingsTransactionType.BankTransfer, notification.Value, notification.PaymentDate, notification.RecipientName, notification.Details, notification.BankTransferId);
                return savingsTransaction;
            }

            return null;
        }

        public async Task<List<SavingsTransaction>> CreateSavingsTransactionsWhen(SavingsAccountAdded notification)
        {
            var expenses = await _expenseRepository.FindByIban(notification.Iban);
            var result = expenses
                .Select(x => _savingsTransactionFactory.CreateFrom(SavingsTransactionType.BankTransfer, x.Value, x.ExpenseDate, x.Details1, x.Details2, x.ExpenseSourceId))
                .ToList();

            return result;
        }

        public SavingsTransaction CreateSavingsTransactionWhen(RoundUpAdded notification)
        {
            var savingsTransaction = _savingsTransactionFactory.CreateFrom(SavingsTransactionType.RoundUp, notification.Value, notification.PaymentDate, notification.Iban, null, notification.RoundUpId);
            return savingsTransaction;
        }
    }
}
