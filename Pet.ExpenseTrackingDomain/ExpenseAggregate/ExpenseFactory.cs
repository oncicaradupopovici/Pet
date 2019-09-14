using System;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate
{
    public class ExpenseFactory
    {
        private readonly ExpenseMonthService _expenseMonthService;

        public ExpenseFactory(ExpenseMonthService expenseMonthService)
        {
            _expenseMonthService = expenseMonthService;
        }

        public Expense CreateFrom(ExpenseType expenseType, decimal value, DateTime expenseDate, Guid? expenseRecipientId, int? expenseCategoryId, string expenseRecipientDetailCode, string details1, string details2, string sourceCategory, Guid? expenseSourceId)
        {
            var expenseMonth = _expenseMonthService.GetExpenseMonthByDate(expenseDate);
            return new Expense(expenseType, value, expenseDate, expenseMonth, expenseRecipientId, expenseCategoryId, expenseRecipientDetailCode, details1, details2, sourceCategory, expenseSourceId);
        }
    }
}
