using NBB.Domain;
using Pet.Banking.Domain.ExchangeAggregate.DomainEvents;
using System;

namespace Pet.Banking.Domain.ExchangeAggregate
{
    public class Exchange : EventedAggregateRoot<Guid>
    {
        public Guid ExchangeId { get; private set; }
        public string Iban { get; private set; }
        public string ExchangeValue { get; private set; }
        public string ExchangeRate { get; private set; }
        public string Details { get; private set; }
        public decimal Value { get; private set; }
        public DateTime PaymentDate { get; private set; }

        //for ef
        private Exchange()
        {

        }

        public Exchange( string iban, string exchangeValue, string exchangeRate, string details, decimal value, DateTime paymentDate)
        {
            ExchangeId = Guid.NewGuid();
            Iban = iban;
            ExchangeValue = exchangeValue;
            ExchangeRate = exchangeRate;
            Details = details;
            Value = value;
            PaymentDate = paymentDate;

            AddEvent(new ExchangeAdded(ExchangeId, Iban, ExchangeValue, ExchangeRate, Details, Value, PaymentDate));
        }

        public override Guid GetIdentityValue() => ExchangeId;
    }
}
