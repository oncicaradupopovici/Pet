using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record ExpenseRecipientAdded(Guid ExpenseRecipientId, string Name) : INotification;
}
