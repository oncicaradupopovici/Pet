using System;
using NBB.Domain;

namespace Pet.Banking.Domain.CollectionAggregate.DomainEvents
{
    public class CollectionAdded : DomainEvent
    {
        public Guid CollectionId { get;  }
        public string From { get;  }
        public string FromIban { get; }
        public string Details { get;  }
        public decimal Value { get;  }
        public DateTime IncomeDate { get; }

        public CollectionAdded(Guid collectionId, string from, string fromIban, string details, decimal value, DateTime incomeDate, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            CollectionId = collectionId;
            From = from;
            FromIban = fromIban;
            Details = details;
            Value = value;
            IncomeDate = incomeDate;
        }
    }
}
