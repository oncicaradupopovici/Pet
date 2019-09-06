using System;
using NBB.Domain;

namespace Pet.Banking.Domain.BankTransferAggregate.DomainEvents
{
    public class BankTransferAdded : DomainEvent
    {
        public Guid BankTransferId { get; }
        public string Iban { get; }
        public string RecipientName { get; }
        public string Details { get; }
        public decimal Value { get; }
        public DateTime PaymentDate { get; }

        public BankTransferAdded(Guid bankTransferId, string iban, string recipientName, string details, decimal value, DateTime paymentDate, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            BankTransferId = bankTransferId;
            Iban = iban;
            RecipientName = recipientName;
            Details = details;
            Value = value;
            PaymentDate = paymentDate;
        }
    }
}
