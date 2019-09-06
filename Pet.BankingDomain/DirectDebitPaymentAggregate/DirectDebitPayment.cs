using System;
using NBB.Domain;
using Pet.Banking.Domain.DirectDebitPaymentAggregate.DomainEvents;

namespace Pet.Banking.Domain.DirectDebitPaymentAggregate
{
    public class DirectDebitPayment : EventedAggregateRoot<Guid>
    {
        public Guid DirectDebitPaymentId { get; private set;}
        public string DirectDebitCode { get; private set;}
        public decimal Value { get; private set;}
        public string Details { get; private set; }
        public DateTime PaymentDate { get; private set;}

        //for ef
        private DirectDebitPayment()
        {

        }

        public DirectDebitPayment(string directDebitCode, decimal value, string details, DateTime paymentDate)
        {
            DirectDebitPaymentId = Guid.NewGuid();
            DirectDebitCode = directDebitCode;
            Value = value;
            Details = details;
            PaymentDate = paymentDate;

            AddEvent(new DirectDebitPaymentAdded(DirectDebitPaymentId, DirectDebitCode, Value, Details, PaymentDate));
        }

        public override Guid GetIdentityValue() => DirectDebitPaymentId;
    }
}
