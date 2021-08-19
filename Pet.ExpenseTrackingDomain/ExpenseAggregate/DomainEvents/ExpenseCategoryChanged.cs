using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public record ExpenseCategoryChanged(Guid ExpenseId, int? ExpenseCategoryId, int ExpenseMonth) : INotification;
}
