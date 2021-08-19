using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public record ExpenseRecipientChanged(Guid ExpenseId, Guid? ExpenseRecipientId, int ExpenseMonth) : INotification;
}
