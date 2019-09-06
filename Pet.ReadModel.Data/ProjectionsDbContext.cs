using Microsoft.EntityFrameworkCore;
using Pet.ReadModel.Data.EntityTypeConfiguration;

namespace Pet.ReadModel.Data
{
    class ProjectionsDbContext : DbContext
    {
        public ProjectionsDbContext(DbContextOptions<ProjectionsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseByRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseByCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseMonthConfiguration());
        }
    }
}
