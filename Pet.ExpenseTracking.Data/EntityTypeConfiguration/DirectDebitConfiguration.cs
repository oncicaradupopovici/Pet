using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class DirectDebitConfiguration : IEntityTypeConfiguration<DirectDebit>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DirectDebit> builder)
        {
            builder.ToTable("DirectDebit").HasKey(x => x.Code);
        }
    }

}
