using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions.Linq;
using Pet.ExpenseTracking.Domain.Services;
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
            private readonly ExpenseMonthService _expenseMonthService;

            public QueryHandler(IQueryable<ExpenseMonth> query, ExpenseMonthService expenseMonthService)
            {
                _query = query;
                _expenseMonthService = expenseMonthService;
            }

            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _query.ToListAsync(cancellationToken);
                var result = items.Select(x => new Model(x.ExpenseMonthId, x.TotalExpenses, x.TotalSavings, _expenseMonthService.GetExpenseMonthName(x.ExpenseMonthId)))
                    .OrderByDescending(x => x.ExpenseMonthId).ToList();

                return result;
            }
        }
    }
}
