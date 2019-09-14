using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public class ExpenseAdded : DomainEvent
    {
        public Guid ExpenseId { get; }
        public ExpenseType ExpenseType { get; }
        public decimal Value { get; }
        public DateTime ExpenseDate { get; }
        public Guid? ExpenseRecipientId { get; }
        public int? ExpenseCategoryId { get; }
        public string ExpenseRecipientDetailCode { get; }
        public string Details1 { get; }
        public string Details2 { get; }
        public string SourceCategory { get; }
        public Guid? ExpenseSourceId { get; }
        public int ExpenseMonth { get; }

        public ExpenseAdded(Guid expenseId, ExpenseType expenseType, decimal value, DateTime expenseDate, Guid? expenseRecipientId, int? expenseCategoryId, string expenseRecipientDetailCode, string details1, string details2, string sourceCategory, Guid? expenseSourceId, int expenseMonth,  DomainEventMetadata metadata = null)
            : base(metadata)
        {
            ExpenseId = expenseId;
            ExpenseType = expenseType;
            Value = value;
            ExpenseDate = expenseDate;
            ExpenseRecipientId = expenseRecipientId;
            ExpenseCategoryId = expenseCategoryId;
            ExpenseRecipientDetailCode = expenseRecipientDetailCode;
            ExpenseSourceId = expenseSourceId;
            ExpenseMonth = expenseMonth;
            Details1 = details1;
            Details2 = details2;
            SourceCategory = sourceCategory;
        }
    }
}
