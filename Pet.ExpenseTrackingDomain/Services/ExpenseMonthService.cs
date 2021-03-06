﻿using System;

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

        public int GetFisrtExpenseMonthOfCurrentYear()
        {
            var today = DateTime.Today;
            var expensePeriodFirstDayOfMonth = _expenseSettingsRepository.GetExpensePeriodFirstDayOfMonth();
            var currentYearFirstMonth = new DateTime(today.Year, 1, expensePeriodFirstDayOfMonth);
            var expenseMonth = GetExpenseMonthByDate(currentYearFirstMonth);
            return expenseMonth;
        }

        public int GetExpenseMonthForNMonthsAgo(int months)
        {
            var today = DateTime.Today;
            var date = today.AddMonths(-months);
            var result = GetExpenseMonthByDate(date);
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
