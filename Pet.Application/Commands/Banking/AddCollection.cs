using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.CollectionAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddCollection
    {
        public class Command :  NBB.Application.DataContracts.Command, IHaveTransactionDate
        {
            public string From { get; }
            public string FromIban { get; }
            public string Details { get; }
            public decimal Value { get; }
            public DateTime IncomeDate { get; }

            DateTime IHaveTransactionDate.TransactionDate => IncomeDate;

            public Command(string from, string fromIban, string details, decimal value, DateTime incomeDate, CommandMetadata metadata = null)
                : base(metadata)
            {
                From = from;
                FromIban = fromIban;
                Details = details;
                Value = value;
                IncomeDate = incomeDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICollectionRepository _repository;

            public Handler(ICollectionRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var collection = new Collection(request.From, request.FromIban, request.Details, request.Value, request.IncomeDate);
                await _repository.AddAsync(collection);
                await _repository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
