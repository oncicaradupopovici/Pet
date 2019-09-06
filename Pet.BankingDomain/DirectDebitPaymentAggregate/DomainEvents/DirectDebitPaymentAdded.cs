using System;
using NBB.Domain;

namespace Pet.Banking.Domain.DirectDebitPaymentAggregate.DomainEvents
{
    public class DirectDebitPaymentAdded : DomainEvent
    {
        public Guid DirectDebitPaymentId { get; }
        public string DirectDebitCode { get; }
        public decimal Value { get; }
        public string Details { get; }
        public DateTime PaymentDate { get; }

        public DirectDebitPaymentAdded(Guid directDebitPaymentId, string code, decimal value, string details, DateTime paymentDate, DomainEventMetadata metadata = null)
            :base(metadata)
        {
            DirectDebitPaymentId = directDebitPaymentId;
            DirectDebitCode = code;
            Value = value;
            Details = details;
            PaymentDate = paymentDate;
        }
    }
}
