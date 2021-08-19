using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record PosTerminalAdded(string PosTerminal, Guid ExpenseRecipientId) : INotification;
}
