using Microsoft.EntityFrameworkCore;
using Pet.Banking.Domain.CashWithdrawalAggregate;

namespace Pet.Banking.Data.EntityTypeConfiguration
{
    class CashWithdrawalConfiguration : IEntityTypeConfiguration<CashWithdrawal>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CashWithdrawal> builder)
        {
            builder.ToTable("CashWithdrawal").HasKey(x => x.CashWithdrawalId);
            builder.Ignore(x => x.Version);
        }
    }

}
