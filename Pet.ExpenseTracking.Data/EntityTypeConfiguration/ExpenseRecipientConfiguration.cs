using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    public class ExpenseRecipientConfiguration : IEntityTypeConfiguration<ExpenseRecipient>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExpenseRecipient> builder)
        {
            builder.ToTable("ExpenseRecipient").HasKey(x => x.ExpenseRecipientId);
            builder.HasMany(x => x.PosTerminals).WithOne();
            builder.HasMany(x => x.Ibans).WithOne();
            builder.HasMany(x => x.DirectDebits).WithOne();
            builder.HasMany(x => x.OpenBankingMerchants).WithOne();
            builder.Ignore(x => x.Version);
        }
    }

}
