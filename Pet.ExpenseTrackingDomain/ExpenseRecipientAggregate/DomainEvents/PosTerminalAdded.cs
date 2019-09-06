using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class PosTerminalAdded : DomainEvent
    {
        public string PosTerminal { get; }
        public Guid ExpenseRecipientId { get; }

        public PosTerminalAdded(string posTerminal, Guid expenseRecipientId, DomainEventMetadata metadata = null) : base(metadata)
        {
            PosTerminal = posTerminal;
            ExpenseRecipientId = expenseRecipientId;
        }
    }
}
