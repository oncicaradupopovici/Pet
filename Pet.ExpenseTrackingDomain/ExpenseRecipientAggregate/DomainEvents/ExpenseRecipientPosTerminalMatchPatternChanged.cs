using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record ExpenseRecipientPosTerminalMatchPatternChanged(Guid ExpenseRecipientId,
        string PosTerminalMatchPattern) : INotification;
}
