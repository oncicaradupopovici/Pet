using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class SavingsAccountConfiguration : IEntityTypeConfiguration<SavingsAccount>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SavingsAccount> builder)
        {
            builder.ToTable("SavingsAccount").HasKey(x => x.Iban);
            builder.Ignore(x => x.Version);
        }
    }

}
