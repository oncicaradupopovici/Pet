using System;
using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;

namespace Pet.ExpenseTracking.Data.EntityTypeConfiguration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expense").HasKey(x => x.ExpenseId);
            builder.Property(c => c.ExpenseType)
                .HasConversion<Byte>();
            builder.Ignore(x => x.Version);
        }
    }

}
