﻿using MediatR;
using Pet.ExpenseTracking.Domain.Services;
using Pet.ReadModel.Projections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseByCategoryInRangeList
    {
        public class Query : IRequest<List<Model>>
        {
            public int? FromExpenseMonthId { get; set; }
            public int? ToExpenseMonthId { get; set; }
        }

        public class Model
        {
            public class CategoryModel
            {
                public int ExpenseCategoryId { get; }
                public string Name { get; }

                public CategoryModel(int expenseCategoryId, string name)
                {
                    ExpenseCategoryId = expenseCategoryId;
                    Name = name;
                }
            }

            public long Id { get; }
            public decimal Value { get; }
            public int ExpenseMonth { get; set; }
            public string ExpenseMonthName { get; }
            public CategoryModel ExpenseCategory { get; }

            public Model(long id, decimal value, int expenseMonth, string expenseMonthName, CategoryModel expenseCategory)
            {
                Id = id;
                Value = value;
                ExpenseCategory = expenseCategory;
                ExpenseMonth = expenseMonth;
                ExpenseMonthName = expenseMonthName;
            }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IAsyncEnumerable<ExpenseByCategory> _query;
            private readonly ExpenseMonthService _expenseMonthService;

            public QueryHandler(IAsyncEnumerable<ExpenseByCategory> query, ExpenseMonthService expenseMonthService)
            {
                _query = query;
                _expenseMonthService = expenseMonthService;
            }

            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _query;

                var fromExpenseMonthId = request.FromExpenseMonthId.HasValue
                    ? request.FromExpenseMonthId.Value
                    : _expenseMonthService.GetExpenseMonthForNMonthsAgo(5);

                query = query.Where(q => q.ExpenseMonth >= fromExpenseMonthId);

                if (request.ToExpenseMonthId.HasValue)
                {
                    query = query.Where(q => q.ExpenseMonth <= request.ToExpenseMonthId);
                }

                var items = await query.ToListAsync(cancellationToken);
                var result = items.Select(x => new Model(
                    x.Id,
                    x.Value,
                    x.ExpenseMonth,
                    _expenseMonthService.GetExpenseMonthName(x.ExpenseMonth),
                    x.ExpenseCategoryId.HasValue ? new Model.CategoryModel(x.ExpenseCategoryId.Value, x.ExpenseCategoryName) : null
                    )).OrderBy(x => x.ExpenseMonth).ToList();

                return result;
            }
        }
    }
}
