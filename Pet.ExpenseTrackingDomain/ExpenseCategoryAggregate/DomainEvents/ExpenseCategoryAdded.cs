using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate.DomainEvents
{
    public class ExpenseCategoryAdded : DomainEvent
    {
        public int ExpenseCategoryId { get; }
        public string Name { get; }

        public ExpenseCategoryAdded(int expenseCategoryId, string name, DomainEventMetadata metadata = null) : base(metadata)
        {
            ExpenseCategoryId = expenseCategoryId;
            Name = name;
        }
    }
}
