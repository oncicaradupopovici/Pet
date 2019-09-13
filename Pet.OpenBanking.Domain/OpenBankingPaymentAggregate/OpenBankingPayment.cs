using System;
using NBB.Domain;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate.DomainEvents;

namespace Pet.OpenBanking.Domain.OpenBankingPaymentAggregate
{
    public class OpenBankingPayment : EventedAggregateRoot<Guid>
    {
        public Guid OpenBankingPaymentId { get; private set;}
        public decimal Value { get; private set;}
        public DateTime PaymentDate { get; private set;}
        public string Currency { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public string Merchant { get; private set;}
        public string Category { get; private set; }
        public string Location { get; private set; }


        //for ef
        private OpenBankingPayment()
        {

        }

        public OpenBankingPayment(Guid openBankingPaymentId, decimal value, DateTime paymentDate, string currency, decimal exchangeRate, string merchant, string category, string location)
        {
            OpenBankingPaymentId = openBankingPaymentId;
            Value = value;
            PaymentDate = paymentDate;
            Currency = currency;
            ExchangeRate = exchangeRate;
            Merchant = merchant;
            Category = category;
            Location = location;

            AddEvent(new OpenBankingPaymentAdded(OpenBankingPaymentId, Value, PaymentDate, Currency, ExchangeRate, Merchant, Category, Location));
        }

        public override Guid GetIdentityValue() => OpenBankingPaymentId;
    }
}
