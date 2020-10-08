using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.DirectDebitPaymentAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddDirectDebitPayment
    {
        public class Command :  NBB.Application.DataContracts.Command, IHaveTransactionDate
        {
            public string DirectDebitCode { get; }
            public decimal Value { get; }
            public string Details { get; }
            public DateTime PaymentDate { get; }

            DateTime IHaveTransactionDate.TransactionDate => PaymentDate;

            public Command(string directDebitCode, decimal value, string details, DateTime paymentDate, CommandMetadata metadata = null) : base(metadata)
            {
                DirectDebitCode = directDebitCode;
                Value = value;
                Details = details;
                PaymentDate = paymentDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDirectDebitPaymentRepository _repository;

            public Handler(IDirectDebitPaymentRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var directDebitPayment = new DirectDebitPayment(request.DirectDebitCode, request.Value, request.Details, request.PaymentDate);
                await _repository.AddAsync(directDebitPayment);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
