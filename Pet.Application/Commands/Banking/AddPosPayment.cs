using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.PosPaymentAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddPosPayment
    {
        public class Command :  NBB.Application.DataContracts.Command
        {
            public string PosTerminalCode { get; }
            public decimal Value { get; }
            public DateTime PaymentDate { get; }

            public Command(string posTerminalCode, decimal value, DateTime paymentDate, CommandMetadata metadata = null) : base(metadata)
            {
                PosTerminalCode = posTerminalCode;
                Value = value;
                PaymentDate = paymentDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IPosPaymentRepository _repository;

            public Handler(IPosPaymentRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var posPayment = new PosPayment(request.PosTerminalCode, request.Value, request.PaymentDate);
                await _repository.AddAsync(posPayment);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
