using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class SavingsCategoryConfiguration : IEntityTypeConfiguration<SavingsCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SavingsCategory> builder)
        {
            builder.ToTable("SavingsCategory").HasKey(x => x.Category);
            builder.Ignore(x => x.Version);
        }
    }

}
