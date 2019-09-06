using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class ExpenseRecipientCategoryChanged : DomainEvent
    {
        public Guid ExpenseRecipientId { get; }
        public int? OldExpenseCategoryId { get; }

        public int? NewExpenseCategoryId { get; }
        public int ExpenseMonth { get; }

        public ExpenseRecipientCategoryChanged(Guid expenseRecipientId, int? oldExpenseCategoryId, int? newExpenseCategoryId, int expenseMonth, DomainEventMetadata metadata = null) : base(metadata)
        {
            ExpenseRecipientId = expenseRecipientId;
            OldExpenseCategoryId = oldExpenseCategoryId;
            NewExpenseCategoryId = newExpenseCategoryId;
            ExpenseMonth = expenseMonth;
        }
    }
}
