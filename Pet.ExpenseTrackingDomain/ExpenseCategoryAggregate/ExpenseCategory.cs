using NBB.Domain;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate
{
    public class ExpenseCategory : EventedAggregateRoot<int>
    {
        public int ExpenseCategoryId { get; set; }
        public string Name { get; set; }

        //for ef
        private ExpenseCategory()
        {
        }

        public ExpenseCategory(string name)
        {
            Name = name;

            AddEvent(new ExpenseCategoryAdded(ExpenseCategoryId, Name));
        }

        public override int GetIdentityValue() => ExpenseCategoryId;
    }
}
