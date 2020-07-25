using NBB.Domain;
using Pet.Banking.Domain.RoundUpAggregate.DomainEvents;
using System;

namespace Pet.Banking.Domain.RoundUpAggregate
{
    public class RoundUp : EventedAggregateRoot<Guid>
    {
        public Guid RoundUpId { get; private set; }
        public string Iban { get; private set; }
        public decimal Value { get; private set; }
        public DateTime PaymentDate { get; private set; }

        //for ef
        private RoundUp()
        {

        }

        public RoundUp(string iban, decimal value, DateTime paymentDate)
        {
            RoundUpId = Guid.NewGuid();
            Iban = iban;
            Value = value;
            PaymentDate = paymentDate;

            AddEvent(new RoundUpAdded(RoundUpId, Iban, Value, PaymentDate));

        }

        public override Guid GetIdentityValue() => RoundUpId;
    }
}
