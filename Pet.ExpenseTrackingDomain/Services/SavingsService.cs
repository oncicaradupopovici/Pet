using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pet.Banking.Domain.BankTransferAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class SavingsService
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly ISavingsCategoryRepository _savingsCategoryRepository;
        private readonly SavingsTransactionFactory _savingsTransactionFactory;
        private readonly IExpenseRepository _expenseRepository;

        public SavingsService(ISavingsAccountRepository savingsAccountRepository, ISavingsCategoryRepository savingsCategoryRepository, SavingsTransactionFactory savingsTransactionFactory, IExpenseRepository expenseRepository)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _savingsCategoryRepository = savingsCategoryRepository;
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

        public async Task<SavingsTransaction> CreateSavingsTransactionWhen(OpenBankingPaymentAdded notification)
        {
            var isSavingsCategory =
                await _savingsCategoryRepository.IsSavingsCategory(notification.Category);
            if (isSavingsCategory)
            {
                var savingsTransaction = _savingsTransactionFactory.CreateFrom(SavingsTransactionType.OpenBankingPayment, notification.Value, notification.PaymentDate, notification.Merchant, notification.Location, notification.OpenBankingPaymentId);
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

        public async Task<List<SavingsTransaction>> CreateSavingsTransactionsWhen(SavingsCategoryAdded notification)
        {
            var expenses = await _expenseRepository.FindBySourceCategory(notification.Category);
            var result = expenses
                .Select(x => _savingsTransactionFactory.CreateFrom(SavingsTransactionType.OpenBankingPayment, x.Value, x.ExpenseDate, x.Details1, x.Details2, x.ExpenseSourceId))
                .ToList();

            return result;
        }
    }
}
