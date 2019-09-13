using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet.ReadModel.Projections;

namespace Pet.ReadModel.Data.EntityTypeConfiguration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("vwExpense").HasKey(x => x.ExpenseId);
        }
    }
}
