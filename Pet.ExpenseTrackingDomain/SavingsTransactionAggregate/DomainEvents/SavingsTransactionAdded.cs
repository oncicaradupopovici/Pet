using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.SavingsTransactionAggregate.DomainEvents
{
    public record SavingsTransactionAdded(Guid SavingsTransactionId, SavingsTransactionType SavingsTransactionType,
        decimal Value, DateTime TransactionDate, int TransactionMonth, Guid? SourceId) : INotification;
}
