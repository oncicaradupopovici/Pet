using System;
using NBB.Domain;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.SavingsTransactionAggregate
{
    public class SavingsTransaction : EventedAggregateRoot<Guid>
    {
        public Guid SavingsTransactionId { get; private set; }
        public SavingsTransactionType SavingsTransactionType { get; private set; }
        public decimal Value { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public int TransactionMonth { get; private set; }
        public string Details1 { get; private set; }
        public string Details2 { get; private set; }

        public Guid? SourceId { get; private set; }

        private SavingsTransaction()
        {
        }

        internal SavingsTransaction(SavingsTransactionType savingsTransactionType, decimal value, DateTime transactionDate, int transactionMonthmonth, string details1, string details2, Guid? sourceId)
        {
            SavingsTransactionId = Guid.NewGuid();
            SavingsTransactionType = savingsTransactionType;
            Value = value;
            TransactionDate = transactionDate;
            TransactionMonth = transactionMonthmonth;
            Details1 = details1;
            Details2 = details2;
            SourceId = sourceId;

            AddEvent(new SavingsTransactionAdded(SavingsTransactionId, SavingsTransactionType, Value, TransactionDate, TransactionMonth, SourceId));
        }

        public override Guid GetIdentityValue() => SavingsTransactionId;
    }
}
