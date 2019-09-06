using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using NBB.Data.Abstractions.Linq;
using Pet.ReadModel.Projections;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseByCategoryList
    {
        public class Query : Query<List<Model>>
        {
            public int ExpenseMonthId { get; set; }
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
            public CategoryModel ExpenseCategory { get; }

            public Model(long id, decimal value, CategoryModel expenseCategory)
            {
                Id = id;
                Value = value;
                ExpenseCategory = expenseCategory;
            }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IQueryable<ExpenseByCategory> _query;

            public QueryHandler(IQueryable<ExpenseByCategory> query)
            {
                _query = query;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _query.Where(x => x.ExpenseMonth == request.ExpenseMonthId).ToListAsync(cancellationToken);
                var result = items.Select(x => new Model(
                    x.Id,
                    x.Value,
                    x.ExpenseCategoryId.HasValue ? new Model.CategoryModel(x.ExpenseCategoryId.Value, x.ExpenseCategoryName) : null
                    )).OrderByDescending(x=>x.Value).ToList();

                return result;
            }
        }
    }
}
