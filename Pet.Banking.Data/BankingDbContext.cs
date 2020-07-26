using Microsoft.EntityFrameworkCore;
using Pet.Banking.Data.EntityTypeConfiguration;

namespace Pet.Banking.Data
{
    class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PosPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new DirectDebitPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new BankTransferConfiguration());
            modelBuilder.ApplyConfiguration(new CashWithdrawalConfiguration());
            modelBuilder.ApplyConfiguration(new RoundUpConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
        }
    }
}
