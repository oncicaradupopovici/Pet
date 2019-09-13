using Microsoft.EntityFrameworkCore;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate;

namespace Pet.OpenBanking.Data.EntityTypeConfiguration
{
    class OpenBankingPaymentConfiguration : IEntityTypeConfiguration<OpenBankingPayment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OpenBankingPayment> builder)
        {
            builder.ToTable("OpenBankingPayment").HasKey(x => x.OpenBankingPaymentId);
            builder.Ignore(x => x.Version);
        }
    }

}
