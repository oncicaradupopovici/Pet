using NBB.Domain;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.SavingsCategoryAggregate
{
    public class SavingsCategory : EventedAggregateRoot<string>
    {
        public string Category { get; private set; }

        private SavingsCategory()
        {
        }

        public SavingsCategory(string category)
        {
            Category = category;
            AddEvent(new SavingsCategoryAdded(Category));
        }

        public override string GetIdentityValue() => Category;
    }
}
