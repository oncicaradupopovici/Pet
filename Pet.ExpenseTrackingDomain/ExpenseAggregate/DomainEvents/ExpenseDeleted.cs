using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public class ExpenseDeleted : DomainEvent
    {
        public Guid ExpenseId { get; }
        public int ExpenseMonth { get; }

        public ExpenseDeleted(Guid expenseId, int expenseMonth, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            ExpenseId = expenseId;
            ExpenseMonth = expenseMonth;
        }
    }
}
