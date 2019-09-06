using System;
using NBB.Domain;
using Pet.Banking.Domain.PosPaymentAggregate.DomainEvents;

namespace Pet.Banking.Domain.PosPaymentAggregate
{
    public class PosPayment : EventedAggregateRoot<Guid>
    {
        public Guid PosPaymentId { get; private set;}
        public string PosTerminalCode { get; private set;}
        public decimal Value { get; private set;}
        public DateTime PaymentDate { get; private set;}

        //for ef
        private PosPayment()
        {

        }

        public PosPayment(string posTerminalCode, decimal value, DateTime paymentDate)
        {
            PosPaymentId = Guid.NewGuid();
            PosTerminalCode = posTerminalCode;
            Value = value;
            PaymentDate = paymentDate;

            AddEvent(new PosPaymentAdded(PosPaymentId, PosTerminalCode, Value, PaymentDate));
        }

        public override Guid GetIdentityValue() => PosPaymentId;
    }
}
