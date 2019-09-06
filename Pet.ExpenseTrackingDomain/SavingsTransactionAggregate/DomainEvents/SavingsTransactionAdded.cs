using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.SavingsTransactionAggregate.DomainEvents
{
    public class SavingsTransactionAdded : DomainEvent
    {
        public Guid SavingsTransactionId { get; }
        public SavingsTransactionType SavingsTransactionType { get; }
        public decimal Value { get; }
        public DateTime TransactionDate { get; }
        public int TransactionMonth { get; }
        public Guid? SourceId { get; }

        public SavingsTransactionAdded(Guid savingsTransactionId, SavingsTransactionType savingsTransactionType, decimal value, DateTime transactionDate, int transactionMonth, Guid? sourceId, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            SavingsTransactionId = savingsTransactionId;
            SavingsTransactionType = savingsTransactionType;
            Value = value;
            TransactionDate = transactionDate;
            TransactionMonth = transactionMonth;
            SourceId = sourceId;
        }
    }
}
