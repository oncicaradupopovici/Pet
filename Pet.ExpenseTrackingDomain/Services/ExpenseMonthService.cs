using System;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class ExpenseMonthService
    {
        private readonly ExpenseSettingsService _expenseSettingsRepository;

        public ExpenseMonthService(ExpenseSettingsService expenseSettingsRepository)
        {
            _expenseSettingsRepository = expenseSettingsRepository;
        }

        public int GetExpenseMonthByDate(DateTime someDate)
        {
            var expensePeriodFirstDayOfMonth = _expenseSettingsRepository.GetExpensePeriodFirstDayOfMonth();
            var resultDate = someDate.Day < expensePeriodFirstDayOfMonth ? someDate.AddMonths(-1) : someDate;
            var result = resultDate.Year * 100 + resultDate.Month;
            return result;
        }

        public string GetExpenseMonthName(int expenseMonthId)
        {
            var year = expenseMonthId / 100;
            var month = expenseMonthId % 100;
            var date = new DateTime(year, month, 1);
            return date.ToString("MMMM yyyy");
        }
    }
}
