using System;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.ExpenseTracking.Domain.IncomeAggregate
{
    public class IncomeFactory
    {
        private readonly ExpenseMonthService _expenseMonthService;

        public IncomeFactory(ExpenseMonthService expenseMonthService)
        {
            _expenseMonthService = expenseMonthService;
        }

        public Income CreateFrom(IncomeType incomeType, decimal value, DateTime incomeDate, string from, string details1, string details2, Guid? sourceId)
        {
            var incomeMonth = _expenseMonthService.GetExpenseMonthByDate(incomeDate);
            return new Income(incomeType, value, incomeDate, incomeMonth, from, details1, details2, sourceId);
        }
    }
}
