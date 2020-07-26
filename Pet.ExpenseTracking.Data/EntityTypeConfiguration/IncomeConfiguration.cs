using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.IncomeAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Income").HasKey(x => x.IncomeId);
            builder.Ignore(x => x.Version);
        }
    }

}
