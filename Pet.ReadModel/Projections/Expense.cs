using System;

namespace Pet.ReadModel.Projections
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public Byte ExpenseType { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseMonth { get; set; }
        public Guid? ExpenseRecipientId { get; set; }
        public string ExpenseRecipientName { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public Guid? ExpenseSourceId { get; set; }
        public string ExpenseRecipientDetailCode { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
    }
}
