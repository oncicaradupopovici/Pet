using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public class ExpenseRecipientChanged : DomainEvent
    {
        public Guid ExpenseId { get; }
        public Guid? ExpenseRecipientId { get; }
        public int ExpenseMonth { get; }

        public ExpenseRecipientChanged(Guid expenseId, Guid? expenseRecipientId, int expenseMonth, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            ExpenseId = expenseId;
            ExpenseRecipientId = expenseRecipientId;
            ExpenseMonth = expenseMonth;
        }
    }
}
