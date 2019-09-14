using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.SavingsCategoryAggregate.DomainEvents
{
    public class SavingsCategoryAdded : DomainEvent
    {
        public string Category { get; }

        public SavingsCategoryAdded(string category, DomainEventMetadata metadata = null) : base(metadata)
        {
            Category = category;
        }
    }
}
