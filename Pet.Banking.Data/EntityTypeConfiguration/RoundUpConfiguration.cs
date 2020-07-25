using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.RoundUpAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class RoundUpConfiguration : IEntityTypeConfiguration<RoundUp>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RoundUp> builder)
        {
            builder.ToTable("RoundUp").HasKey(x => x.RoundUpId);
            builder.Ignore(x => x.Version);
        }
    }

}
