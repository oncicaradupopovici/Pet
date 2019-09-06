using System;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.ExpenseTracking.Domain.SavingsTransactionAggregate
{
    public class SavingsTransactionFactory
    {
        private readonly ExpenseMonthService _expenseMonthService;

        public SavingsTransactionFactory(ExpenseMonthService expenseMonthService)
        {
            _expenseMonthService = expenseMonthService;
        }

        public SavingsTransaction CreateFrom(SavingsTransactionType savingsTransactionType, decimal value, DateTime transactionDate, string details1, string details2, Guid? sourceId)
        {
            var transactionMonth = _expenseMonthService.GetExpenseMonthByDate(transactionDate);
            return new SavingsTransaction(savingsTransactionType, value, transactionDate, transactionMonth, details1, details2, sourceId);
        }
    }
}
