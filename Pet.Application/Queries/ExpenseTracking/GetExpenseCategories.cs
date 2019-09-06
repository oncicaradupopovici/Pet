using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseCategories
    {
        public class Query : Query<List<Model>>
        {
        }

        public class Model
        {
            public int ExpenseCategoryId { get; set; }
            public string Name { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IExpenseCategoryRepository _repository;

            public QueryHandler(IExpenseCategoryRepository repository)
            {
                _repository = repository;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository.FindAll();
                var result = items.Select(x => new Model
                {
                    ExpenseCategoryId = x.ExpenseCategoryId,
                    Name = x.Name
                }).OrderBy(x => x.Name).ToList();

                return result;
            }
        }
    }
}
