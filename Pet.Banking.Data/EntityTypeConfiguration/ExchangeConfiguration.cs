using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.ExchangeAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Exchange> builder)
        {
            builder.ToTable("Exchange").HasKey(x => x.ExchangeId);
            builder.Ignore(x => x.Version);
        }
    }

}
