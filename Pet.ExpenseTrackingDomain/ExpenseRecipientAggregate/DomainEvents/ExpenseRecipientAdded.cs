using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class ExpenseRecipientAdded : DomainEvent
    {
        public Guid ExpenseRecipientId { get; }
        public string Name { get; }

        public ExpenseRecipientAdded(Guid expenseRecipientId, string name, DomainEventMetadata metadata = null) : base(metadata)
        {
            ExpenseRecipientId = expenseRecipientId;
            Name = name;
        }
    }
}
