using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.BankTransferAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddBankTransfer
    {
        public class Command :  NBB.Application.DataContracts.Command, IHaveTransactionDate
        {
            public string Iban { get; }
            public string RecipientName { get; }
            public string Details { get; }
            public decimal Value { get; }
            public DateTime PaymentDate { get; }

            DateTime IHaveTransactionDate.TransactionDate => PaymentDate;

            public Command(string iban, string recipientName, string details, decimal value, DateTime paymentDate, CommandMetadata metadata = null)
                 : base(metadata)
            {
                Iban = iban;
                RecipientName = recipientName;
                Details = details;
                Value = value;
                PaymentDate = paymentDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IBankTransferRepository _repository;

            public Handler(IBankTransferRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var bankTransfer = new BankTransfer(request.Iban, request.RecipientName, request.Details, request.Value, request.PaymentDate);
                await _repository.AddAsync(bankTransfer);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
