using Microsoft.EntityFrameworkCore;
using Pet.OpenBanking.Data.EntityTypeConfiguration;

namespace Pet.OpenBanking.Data
{
    class OpenBankingDbContext : DbContext
    {
        public OpenBankingDbContext(DbContextOptions<OpenBankingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OpenBankingPaymentConfiguration());
        }
    }
}
