namespace Pet.ReadModel.Projections
{
    public class ExpenseByCategory
    {
        public long Id { get;  set; }
        public decimal Value { get;  set; }
        public int? ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public int ExpenseMonth { get; set; }
    }
}
