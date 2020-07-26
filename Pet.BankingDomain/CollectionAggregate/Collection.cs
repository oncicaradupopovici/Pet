using System;
using NBB.Domain;
using Pet.Banking.Domain.CollectionAggregate.DomainEvents;

namespace Pet.Banking.Domain.CollectionAggregate
{
    public class Collection : EventedAggregateRoot<Guid>
    {
        public Guid CollectionId { get; private set;}
        public string From { get; private set; }
        public string FromIban { get; private set;}
        public string Details { get; private set;}
        public decimal Value { get; private set;}
        public DateTime IncomeDate { get; private set;}

        //for ef
        private Collection()
        {

        }

        public Collection(string from, string fromIban, string details, decimal value, DateTime incomeDate)
        {
            CollectionId = Guid.NewGuid();
            From = from;
            FromIban = fromIban;
            Details = details;
            Value = value;
            IncomeDate = incomeDate;

            AddEvent(new CollectionAdded(CollectionId, From, FromIban, Details, Value, IncomeDate));
        }

        public override Guid GetIdentityValue() => CollectionId;
    }
}
