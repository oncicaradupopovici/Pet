using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class ExpenseRecipientPosTerminalMatchPatternChanged : DomainEvent
    {
        public Guid ExpenseRecipientId { get; }
        public string PosTerminalMatchPattern { get; }

        public ExpenseRecipientPosTerminalMatchPatternChanged(Guid expenseRecipientId, string posTerminalMatchPattern, DomainEventMetadata metadata = null) : base(metadata)
        {
            ExpenseRecipientId = expenseRecipientId;
            PosTerminalMatchPattern = posTerminalMatchPattern;
        }
    }
}
