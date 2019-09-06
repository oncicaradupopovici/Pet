using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet.ReadModel.Projections;

namespace Pet.ReadModel.Data.EntityTypeConfiguration
{
    public class ExpenseByCategoryConfiguration : IEntityTypeConfiguration<ExpenseByCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseByCategory> builder)
        {
            builder.ToTable("vwExpenseByCategory").HasKey(x => x.Id);
        }
    }

}
