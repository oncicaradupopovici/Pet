using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.PosPaymentAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class PosPaymentConfiguration : IEntityTypeConfiguration<PosPayment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PosPayment> builder)
        {
            builder.ToTable("PosPayment").HasKey(x => x.PosPaymentId);
            builder.Ignore(x => x.Version);
        }
    }

}
