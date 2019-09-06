using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.BankTransferAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class BankTransferConfiguration : IEntityTypeConfiguration<BankTransfer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BankTransfer> builder)
        {
            builder.ToTable("BankTransfer").HasKey(x => x.BankTransferId);
            builder.Ignore(x => x.Version);
        }
    }

}
