using System;
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
    public class GetExpenseByRecipientList
    {
        public class Query : Query<List<Model>>
        {
            public int ExpenseMonthId { get; set; }
        }

        public class Model
        {
            public class RecipientModel
            {
                public Guid ExpenseRecipientId { get; }
                public string Name { get; }

                public RecipientModel(Guid expenseRecipientId, string name)
                {
                    ExpenseRecipientId = expenseRecipientId;
                    Name = name;
                }
            }

            public long Id { get; }
            public decimal Value { get; }
            public RecipientModel ExpenseRecipient { get; }

            public Model(long id, decimal value, RecipientModel expenseRecipient)
            {
                Id = id;
                Value = value;
                ExpenseRecipient = expenseRecipient;
            }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IQueryable<ExpenseByRecipient> _query;

            public QueryHandler(IQueryable<ExpenseByRecipient> query)
            {
                _query = query;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _query.Where(x => x.ExpenseMonth == request.ExpenseMonthId).ToListAsync(cancellationToken);
                var result = items.Select(x => new Model(
                    x.Id,
                    x.Value,
                    x.ExpenseRecipientId.HasValue ? new Model.RecipientModel(x.ExpenseRecipientId.Value, x.ExpenseRecipientName) : null
                    )).OrderByDescending(x=>x.Value).ToList();

                return result;
            }
        }
    }
}
