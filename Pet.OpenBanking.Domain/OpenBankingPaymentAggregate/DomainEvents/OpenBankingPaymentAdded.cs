using System;
using NBB.Domain;

namespace Pet.OpenBanking.Domain.OpenBankingPaymentAggregate.DomainEvents
{
    public class OpenBankingPaymentAdded : DomainEvent
    {
        public Guid OpenBankingPaymentId { get; }
        public decimal Value { get; }
        public DateTime PaymentDate { get; }
        public string Currency { get; }
        public decimal ExchangeRate { get; }
        public string Merchant { get; }
        public string Category { get; }
        public string Location { get; }

        public OpenBankingPaymentAdded(Guid openBankingPaymentId, decimal value, DateTime paymentDate, string currency, decimal exchangeRate, string merchant, string category, string location, DomainEventMetadata metadata = null) 
            : base(metadata)
        {
            OpenBankingPaymentId = openBankingPaymentId;
            Value = value;
            PaymentDate = paymentDate;
            Currency = currency;
            ExchangeRate = exchangeRate;
            Merchant = merchant;
            Category = category;
            Location = location;
        }
    }
}
