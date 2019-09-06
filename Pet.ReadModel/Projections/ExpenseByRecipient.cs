using System;

namespace Pet.ReadModel.Projections
{
    public class ExpenseByRecipient
    {
        public long Id { get;  set; }
        public decimal Value { get;  set; }
        public Guid? ExpenseRecipientId { get; set; }
        public string ExpenseRecipientName { get; set; }
        public int ExpenseMonth { get; set; }
    }
}
