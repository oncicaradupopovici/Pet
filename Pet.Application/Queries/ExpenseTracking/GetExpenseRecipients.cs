using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseRecipients
    {
        public class Query : Query<List<Model>>
        {
        }

        public class Model
        {
            public Guid ExpenseRecipientId { get; set; }
            public string Name { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IExpenseRecipientRepository _repository;

            public QueryHandler(IExpenseRecipientRepository repository)
            {
                _repository = repository;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository.FindAll();
                var result = items.Select(x => new Model
                {
                    ExpenseRecipientId = x.ExpenseRecipientId,
                    Name = x.Name
                }).OrderBy(x=> x.Name).ToList();

                return result;
            }
        }
    }
}
