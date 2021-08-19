using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.ExchangeAggregate;
using Pet.Banking.Domain.RoundUpAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddExchange
    {
        public class Command :  NBB.Application.DataContracts.Command, IHaveTransactionDate
        {
            public string Iban { get; }
            public string ExchangeValue { get; }
            public string ExchangeRate { get; }
            public string Details { get; }
            public decimal Value { get; }
            public DateTime PaymentDate { get; }

            DateTime IHaveTransactionDate.TransactionDate => PaymentDate;

            public Command(string iban, string exchangeValue, string exchangeRate, string details, decimal value, DateTime paymentDate, CommandMetadata metadata = null)
                : base(metadata)
            {
                Iban = iban;
                ExchangeValue = exchangeValue;
                ExchangeRate = exchangeRate;
                Details = details;
                Value = value;
                PaymentDate = paymentDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExchangeRepository _repository;

            public Handler(IExchangeRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var exchange = new Exchange(request.Iban, request.ExchangeValue, request.ExchangeRate, request.Details, request.Value, request.PaymentDate);
                await _repository.AddAsync(exchange);
                await _repository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
