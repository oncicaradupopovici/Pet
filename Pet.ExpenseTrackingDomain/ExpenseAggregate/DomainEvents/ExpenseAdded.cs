using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public record ExpenseAdded(Guid ExpenseId, ExpenseType ExpenseType, decimal Value, DateTime ExpenseDate,
        Guid? ExpenseRecipientId, int? ExpenseCategoryId, string ExpenseRecipientDetailCode, string Details1,
        string Details2, Guid? ExpenseSourceId, int ExpenseMonth) : INotification;
}
