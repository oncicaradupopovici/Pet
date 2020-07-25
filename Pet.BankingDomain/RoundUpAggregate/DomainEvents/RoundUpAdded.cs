using System;
using NBB.Domain;

namespace Pet.Banking.Domain.RoundUpAggregate.DomainEvents
{
    public class RoundUpAdded : DomainEvent
    {
        public Guid RoundUpId { get; }
        public string Iban { get; }
        public decimal Value { get; }
        public DateTime PaymentDate { get; }

        public RoundUpAdded(Guid roundUpId, string iban, decimal value, DateTime paymentDate, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            RoundUpId = roundUpId;
            Iban = iban;
            Value = value;
            PaymentDate = paymentDate;
        }
    }
}
