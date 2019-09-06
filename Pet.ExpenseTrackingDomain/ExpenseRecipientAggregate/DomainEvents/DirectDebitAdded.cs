using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class DirectDebitAdded : DomainEvent
    {
        public string Code { get; }
        public Guid ExpenseRecipientId { get; }

        public DirectDebitAdded(string code, Guid expenseRecipientId, DomainEventMetadata metadata = null) : base(metadata)
        {
            Code = code;
            ExpenseRecipientId = expenseRecipientId;
        }
    }
}
