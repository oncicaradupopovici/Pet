using Microsoft.EntityFrameworkCore;
using Pet.ReadModel.Projections;

namespace Pet.ReadModel.Data.EntityTypeConfiguration
{
    public class ExpenseMonthConfiguration : IEntityTypeConfiguration<ExpenseMonth>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExpenseMonth> builder)
        {
            builder.ToTable("vwExpenseMonth").HasKey(x => x.ExpenseMonthId);
        }
    }

}
