using System;
using NBB.Domain;

namespace Pet.Banking.Domain.PosPaymentAggregate.DomainEvents
{
    public class PosPaymentAdded : DomainEvent
    {
        public Guid PosPaymentId { get; }
        public string PosTerminalCode { get; }
        public decimal Value { get; }
        public DateTime PaymentDate { get; }

        public PosPaymentAdded(Guid posPaymentId, string posTerminalCode, decimal value, DateTime paymentDate, DomainEventMetadata metadata = null)
            :base(metadata)
        {
            PosPaymentId = posPaymentId;
            PosTerminalCode = posTerminalCode;
            Value = value;
            PaymentDate = paymentDate;
        }
    }
}
