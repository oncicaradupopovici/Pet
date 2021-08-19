using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record ExpenseRecipientCategoryChanged(Guid ExpenseRecipientId, int? OldExpenseCategoryId,
        int? NewExpenseCategoryId, int ExpenseMonth) : INotification;
}
