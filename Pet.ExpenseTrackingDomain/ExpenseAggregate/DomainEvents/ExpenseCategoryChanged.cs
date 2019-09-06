using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents
{
    public class ExpenseCategoryChanged : DomainEvent
    {
        public Guid ExpenseId { get; }
        public int? ExpenseCategoryId { get; }
        public int ExpenseMonth { get; }

        public ExpenseCategoryChanged(Guid expenseId, int? expenseCategoryId, int expenseMonth, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            ExpenseId = expenseId;
            ExpenseCategoryId = expenseCategoryId;
            ExpenseMonth = expenseMonth;
        }
    }
}
