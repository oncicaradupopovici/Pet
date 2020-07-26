using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.CollectionAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collection").HasKey(x => x.CollectionId);
            builder.Ignore(x => x.Version);
        }
    }

}
