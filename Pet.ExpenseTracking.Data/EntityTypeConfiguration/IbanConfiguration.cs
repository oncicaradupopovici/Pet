using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    class IbanConfiguration : IEntityTypeConfiguration<Iban>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Iban> builder)
        {
            builder.ToTable("Iban").HasKey(x => x.Code);
        }
    }

}
