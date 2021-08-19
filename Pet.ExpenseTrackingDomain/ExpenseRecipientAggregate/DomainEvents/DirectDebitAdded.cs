using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record DirectDebitAdded(string Code, Guid ExpenseRecipientId) : INotification;
}
