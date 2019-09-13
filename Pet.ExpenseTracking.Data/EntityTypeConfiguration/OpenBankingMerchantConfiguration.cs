using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class OpenBankingMerchantConfiguration : IEntityTypeConfiguration<OpenBankingMerchant>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OpenBankingMerchant> builder)
        {
            builder.ToTable("OpenBankingMerchant").HasKey(x => x.Code);
        }
    }

}
