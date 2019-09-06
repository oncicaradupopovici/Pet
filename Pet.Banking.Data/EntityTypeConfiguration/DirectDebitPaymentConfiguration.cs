using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.DirectDebitPaymentAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class DirectDebitPaymentConfiguration : IEntityTypeConfiguration<DirectDebitPayment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DirectDebitPayment> builder)
        {
            builder.ToTable("DirectDebitPayment").HasKey(x => x.DirectDebitPaymentId);
            builder.Ignore(x => x.Version);
        }
    }

}
