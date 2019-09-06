using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class PosTerminalConfiguration : IEntityTypeConfiguration<PosTerminal>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PosTerminal> builder)
        {
            builder.ToTable("PosTerminal").HasKey(x => x.Code);
        }
    }

}
