using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class SavingsTransactionConfiguration : IEntityTypeConfiguration<SavingsTransaction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SavingsTransaction> builder)
        {
            builder.ToTable("SavingsTransaction").HasKey(x => x.SavingsTransactionId);
            builder.Ignore(x => x.Version);
        }
    }

}
