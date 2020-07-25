using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.RoundUpAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddRoundUp
    {
        public class Command :  NBB.Application.DataContracts.Command
        {
            public string Iban { get; }
            public decimal Value { get; }
            public DateTime PaymentDate { get; }

            public Command(string iban, decimal value, DateTime paymentDate, CommandMetadata metadata = null)
                 : base(metadata)
            {
                Iban = iban;
                Value = value;
                PaymentDate = paymentDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRoundUpRepository _repository;

            public Handler(IRoundUpRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var bankTransfer = new RoundUp(request.Iban, request.Value, request.PaymentDate);
                await _repository.AddAsync(bankTransfer);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
