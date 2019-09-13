using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions.Linq;
using Pet.ReadModel.Projections;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseMonthList
    {
        public class Query : Query<List<Model>>
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

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IQueryable<ExpenseMonth> _query;

            public QueryHandler(IQueryable<ExpenseMonth> query)
            {
                _query = query;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _query.ToListAsync(cancellationToken);
                var result = items.Select(x => new Model(x.ExpenseMonthId, x.TotalExpenses, x.TotalSavings, GetExpenseMonthName(x.ExpenseMonthId)))
                    .OrderByDescending(x => x.ExpenseMonthId).ToList();

                return result;
            }

            private string GetExpenseMonthName(int expenseMonthId)
            {
                var year = expenseMonthId / 100;
                var month = expenseMonthId % 100;
                var date = new DateTime(year, month, 1);
                return date.ToString("MMMM yyyy");
            }
        }
    }
}
