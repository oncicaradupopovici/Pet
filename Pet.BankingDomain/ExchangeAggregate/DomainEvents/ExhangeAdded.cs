using System;
using NBB.Domain;

namespace Pet.Banking.Domain.ExchangeAggregate.DomainEvents
{
    public class ExchangeAdded : DomainEvent
    {
        public Guid ExchangeId { get; }
        public string Iban { get; }
        public string ExchangeValue { get; }
        public string ExchangeRate { get; }
        public string Details { get; }
        public decimal Value { get; }
        public DateTime PaymentDate { get; }

        public ExchangeAdded(Guid exchangeId, string iban, string exchangeValue, string exchangeRate, string details, decimal value, DateTime paymentDate, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            ExchangeId = exchangeId;
            Iban = iban;
            ExchangeValue = exchangeValue;
            ExchangeRate = exchangeRate;
            Details = details;
            Value = value;
            PaymentDate = paymentDate;
        }
    }
}
