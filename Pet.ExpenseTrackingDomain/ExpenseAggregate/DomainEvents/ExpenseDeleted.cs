using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public record ExpenseDeleted(Guid ExpenseId, int ExpenseMonth) : INotification;
}
