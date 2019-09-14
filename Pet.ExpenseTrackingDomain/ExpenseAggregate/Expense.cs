using System;
using NBB.Domain;
using Pet.ExpenseTracking.Domain.ExpenseAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate
{
    public class Expense : EventedAggregateRoot<Guid>
    {
        private bool _isDeleted = false;
        public bool IsDeleted() => _isDeleted;
        public Guid ExpenseId { get; private set; }
        public ExpenseType ExpenseType { get; private set; }
        public decimal Value { get; private set; }
        public DateTime ExpenseDate { get; private set; }
        public int ExpenseMonth { get; private set; }

        public Guid? ExpenseRecipientId { get; private set; }
        public int? ExpenseCategoryId { get; private set; }
        public string ExpenseRecipientDetailCode { get; private set; }
        public string Details1 { get; private set; }
        public string Details2 { get; private set; }
        public string SourceCategory { get; private set; }
        public Guid? ExpenseSourceId { get; private set; }



        //needed 4 repository
        private Expense()
        {
        }

        internal Expense(ExpenseType expenseType, decimal value, DateTime expenseDate, int expenseMonth, Guid? expenseRecipientId, int? expenseCategoryId, string expenseRecipientDetailCode, string details1, string details2, string sourceCategory, Guid? expenseSourceId)
        {
            ExpenseId = Guid.NewGuid();
            ExpenseType = expenseType;
            Value = value;
            ExpenseDate = expenseDate;
            ExpenseMonth = expenseMonth;
            ExpenseRecipientId = expenseRecipientId;
            ExpenseCategoryId = expenseCategoryId;
            ExpenseRecipientDetailCode = expenseRecipientDetailCode;
            Details1 = details1;
            Details2 = details2;
            SourceCategory = sourceCategory;
            ExpenseSourceId = expenseSourceId;

            AddEvent(new ExpenseAdded(ExpenseId, ExpenseType, Value, ExpenseDate, ExpenseRecipientId, ExpenseCategoryId, ExpenseRecipientDetailCode, Details1, Details2, SourceCategory,  ExpenseSourceId, ExpenseMonth));
        }


        public void SetExpenseCategory(int? expenseCategoryId)
        {
            if (ExpenseCategoryId != expenseCategoryId)
            {
                ExpenseCategoryId = expenseCategoryId;
                AddEvent(new ExpenseCategoryChanged(ExpenseId, ExpenseCategoryId, ExpenseMonth));
            }
        }

        public void SetExpenseRecipient(Guid expenseRecipientId)
        {
            if (ExpenseRecipientId != expenseRecipientId)
            {
                ExpenseRecipientId = expenseRecipientId;
                AddEvent(new ExpenseRecipientChanged(ExpenseId, ExpenseRecipientId, ExpenseMonth));
            }
        }

        public void Delete()
        {
            _isDeleted = true;
            AddEvent(new ExpenseDeleted(ExpenseId, ExpenseMonth));
        }

        public override Guid GetIdentityValue() => ExpenseId;
    }
}
