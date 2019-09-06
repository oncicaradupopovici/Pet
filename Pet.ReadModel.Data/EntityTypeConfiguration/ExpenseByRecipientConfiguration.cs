using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet.ReadModel.Projections;

namespace Pet.ReadModel.Data.EntityTypeConfiguration
{
    public class ExpenseByRecipientConfiguration : IEntityTypeConfiguration<ExpenseByRecipient>
    {
        public void Configure(EntityTypeBuilder<ExpenseByRecipient> builder)
        {
            builder.ToTable("vwExpenseByRecipient").HasKey(x => x.Id);
        }
    }

}
