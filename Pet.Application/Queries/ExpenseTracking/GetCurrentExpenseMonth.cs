using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pet.ExpenseTracking.Domain.Services;
using Pet.ReadModel.Projections;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetCurrentExpenseMonth
    {
        public class Query : IRequest<Model>
        {
        }

        public class Model
        {
            public int ExpenseMonthId { get; }
            public decimal TotalExpenses { get; }
            public decimal TotalSavings { get; }
            public string Name { get; }

            public Model(int expenseMonthId, decimal totalExpenses, decimal totalSavings, string name)
            {
                ExpenseMonthId = expenseMonthId;
                TotalExpenses = totalExpenses;
                TotalSavings = totalSavings;
                Name = name;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly ExpenseSettingsService _expenseSettingsRepository;
            private readonly IAsyncEnumerable<ExpenseMonth> _expenseMonthQuery;

            public QueryHandler(ExpenseSettingsService expenseSettingsRepository, IAsyncEnumerable<ExpenseMonth> expenseMonthQuery)
            {
                _expenseSettingsRepository = expenseSettingsRepository;
                _expenseMonthQuery = expenseMonthQuery;
            }


            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var expensePeriodFirstDayOfMonth = _expenseSettingsRepository.GetExpensePeriodFirstDayOfMonth();
                var expenseMonth = await _expenseMonthQuery.OrderByDescending(x => x.ExpenseMonthId).Take(1).FirstOrDefaultAsync(cancellationToken);
                var expenseMonthId = expenseMonth?.ExpenseMonthId ?? GetExpenseMonthId(DateTime.Now, expensePeriodFirstDayOfMonth);
                var totalExpenses = expenseMonth?.TotalExpenses ?? 0;
                var totalSavings = expenseMonth?.TotalSavings ?? 0;
                var result = new Model(expenseMonthId, totalExpenses, totalSavings, GetExpenseMonthName(expenseMonthId));

                return result;
            }

            private string GetExpenseMonthName(int expenseMonthId)
            {
                var year = expenseMonthId / 100;
                var month = expenseMonthId % 100;
                var date = new DateTime(year, month, 1);
                return date.ToString("MMMM yyyy");
            }

            private int GetExpenseMonthId(DateTime expenseDate, int expensePeriodFirstDayOfMonth)
            {
                var resultDate = expenseDate.Day < expensePeriodFirstDayOfMonth ? expenseDate.AddMonths(-1) : expenseDate;
                var result = resultDate.Year * 100 + resultDate.Month;
                return result;
            }
        }
    }
}
