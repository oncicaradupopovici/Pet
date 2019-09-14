using Microsoft.EntityFrameworkCore;
using Pet.ExpenseTracking.Data.EntityTypeConfiguration;

namespace Pet.ExpenseTracking.Data
{
    class ExpenseTrackingDbContext : DbContext
    {
        public ExpenseTrackingDbContext(DbContextOptions<ExpenseTrackingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PosTerminalConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new IbanConfiguration());
            modelBuilder.ApplyConfiguration(new DirectDebitConfiguration());
            modelBuilder.ApplyConfiguration(new SavingsAccountConfiguration());
            modelBuilder.ApplyConfiguration(new SavingsTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new OpenBankingMerchantConfiguration());
            modelBuilder.ApplyConfiguration(new SavingsCategoryConfiguration());
        }
    }
}
