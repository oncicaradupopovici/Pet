using System.Collections.Generic;
using System.Threading.Tasks;
using Pet.Banking.Domain.BankTransferAggregate.DomainEvents;
using Pet.Banking.Domain.CashWithdrawalAggregate.DomainEvents;
using Pet.Banking.Domain.DirectDebitPaymentAggregate.DomainEvents;
using Pet.Banking.Domain.PosPaymentAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRecipientRepository _expenseRecipientRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseFactory _expenseFactory;

        public ExpenseService(IExpenseRecipientRepository expenseRecipientRepository, IExpenseRepository expenseRepository, ExpenseFactory expenseFactory)
        {
            _expenseRecipientRepository = expenseRecipientRepository;
            _expenseRepository = expenseRepository;
            _expenseFactory = expenseFactory;
        }
        public async Task<Expense> CreateExpenseWhen(PosPaymentAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindByPosTerminal(notification.PosTerminalCode);

            var expense = _expenseFactory.CreateFrom(ExpenseType.PosPayment, notification.Value, notification.PaymentDate, expenseRecipient?.ExpenseRecipientId, expenseRecipient?.ExpenseCategoryId, notification.PosTerminalCode, notification.PosTerminalCode, null, notification.PosPaymentId);
            return expense;
        }

        public async Task<Expense> CreateExpenseWhen(OpenBankingPaymentAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindByOpenBankingMerchant(notification.Merchant);

            var expense = _expenseFactory.CreateFrom(ExpenseType.OpenBankingPayment, notification.Value, notification.PaymentDate, expenseRecipient?.ExpenseRecipientId, expenseRecipient?.ExpenseCategoryId, notification.Merchant, notification.Merchant, notification.Location, notification.OpenBankingPaymentId);
            return expense;
        }

        public async Task<Expense> CreateExpenseWhen(BankTransferAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindByIban(notification.Iban);

            var expense = _expenseFactory.CreateFrom(ExpenseType.BankTransfer, notification.Value, notification.PaymentDate, expenseRecipient?.ExpenseRecipientId, expenseRecipient?.ExpenseCategoryId, notification.Iban, notification.RecipientName, notification.Details, notification.BankTransferId);
            return expense;
        }

        public async Task<Expense> CreateExpenseWhen(DirectDebitPaymentAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindByDirectDebit(notification.DirectDebitCode);

            var expense = _expenseFactory.CreateFrom(ExpenseType.DirectDebitPayment, notification.Value, notification.PaymentDate, expenseRecipient?.ExpenseRecipientId, expenseRecipient?.ExpenseCategoryId, notification.DirectDebitCode, notification.DirectDebitCode, notification.Details, notification.DirectDebitPaymentId);
            return expense;
        }

        public Task<Expense> CreateExpenseWhen(CashWithdrawalAdded notification)
        {
            var expense = _expenseFactory.CreateFrom(ExpenseType.CashWithdrawal, notification.Value, notification.WithdrawalDate, null, null, null, notification.CashTerminal, null, notification.CashWithdrawalId);
            return Task.FromResult(expense);
        }

        public async Task<List<Expense>> UpdateExpensesWhen(PosTerminalAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindById(notification.ExpenseRecipientId);

            if (expenseRecipient == null)
            {
                return new List<Expense>();
            }

            var expenses = await _expenseRepository.FindByPosTerminal(notification.PosTerminal);
            foreach(var expense in expenses)
            {
                expense.SetExpenseRecipient(expenseRecipient.ExpenseRecipientId);
                expense.SetExpenseCategory(expenseRecipient.ExpenseCategoryId);
            }

            return expenses;
        }

        public async Task<List<Expense>> UpdateExpensesWhen(OpenBankingMerchantAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindById(notification.ExpenseRecipientId);

            if (expenseRecipient == null)
            {
                return new List<Expense>();
            }

            var expenses = await _expenseRepository.FindByOpenBankingMerchant(notification.Code);
            foreach(var expense in expenses)
            {
                expense.SetExpenseRecipient(expenseRecipient.ExpenseRecipientId);
                expense.SetExpenseCategory(expenseRecipient.ExpenseCategoryId);
            }

            return expenses;
        }

        public async Task<List<Expense>> UpdateExpensesWhen(IbanAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindById(notification.ExpenseRecipientId);

            if (expenseRecipient == null)
            {
                return new List<Expense>();
            }

            var expenses = await _expenseRepository.FindByIban(notification.Iban);
            foreach (var expense in expenses)
            {
                expense.SetExpenseRecipient(expenseRecipient.ExpenseRecipientId);
                expense.SetExpenseCategory(expenseRecipient.ExpenseCategoryId);
            }

            return expenses;
        }

        public async Task<List<Expense>> UpdateExpensesWhen(DirectDebitAdded notification)
        {
            var expenseRecipient =
                await _expenseRecipientRepository.FindById(notification.ExpenseRecipientId);

            if (expenseRecipient == null)
            {
                return new List<Expense>();
            }

            var expenses = await _expenseRepository.FindByDirectDebitCode(notification.Code);
            foreach (var expense in expenses)
            {
                expense.SetExpenseRecipient(expenseRecipient.ExpenseRecipientId);
                expense.SetExpenseCategory(expenseRecipient.ExpenseCategoryId);
            }

            return expenses;
        }

        public async Task<List<Expense>> UpdateExpensesWhen(ExpenseRecipientCategoryChanged notification)
        {
            if (!notification.NewExpenseCategoryId.HasValue)
            {
                return new List<Expense>();
            }

            var expenses = await _expenseRepository.FindByExpenseRecipientAndMonth(notification.ExpenseRecipientId, notification.ExpenseMonth);
            foreach (var expense in expenses)
            {
                if (expense.ExpenseCategoryId == notification.OldExpenseCategoryId)
                {
                    expense.SetExpenseCategory(notification.NewExpenseCategoryId);
                }
            }

            return expenses;
        }
    }
}
