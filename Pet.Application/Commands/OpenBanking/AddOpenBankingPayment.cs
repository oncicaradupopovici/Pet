using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate;

namespace Pet.Application.Commands.OpenBanking
{
    public class AddOpenBankingPayment
    {
        public class Command :  NBB.Application.DataContracts.Command
        {
            public Guid OpenBankingPaymentId { get;}
            public decimal Value { get; }
            public DateTime PaymentDate { get; }
            public string Currency { get; }
            public decimal ExchangeRate { get; }
            public string Merchant { get; }
            public string Category { get; }
            public string Location { get; }

            public Command(Guid openBankingPaymentId, decimal value, DateTime paymentDate, string currency, decimal exchangeRate, string merchant, string category, string location, CommandMetadata metadata = null) 
                : base(metadata)
            {
                OpenBankingPaymentId = openBankingPaymentId;
                Value = value;
                PaymentDate = paymentDate;
                Currency = currency;
                ExchangeRate = exchangeRate;
                Merchant = merchant;
                Category = category;
                Location = location;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IOpenBankingPaymentRepository _repository;

            public Handler(IOpenBankingPaymentRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var openBankingPayment = new OpenBankingPayment(request.OpenBankingPaymentId, request.Value, request.PaymentDate, request.Currency, request.ExchangeRate, request.Merchant, request.Category, request.Location);
                await _repository.AddAsync(openBankingPayment);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
