using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.ToTable("ExpenseCategory").HasKey(x => x.ExpenseCategoryId);
            builder.Property(x => x.ExpenseCategoryId).UseIdentityColumn();
            builder.Ignore(x => x.Version);

        }
    }

}
