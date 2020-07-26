using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBB.Core.Abstractions;
using NBB.Data.EntityFramework;
using Pet.ExpenseTracking.Data.Repositories;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;
using Pet.ExpenseTracking.Domain.IncomeAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.Tenant.Abstractions;

namespace Pet.ExpenseTracking.Data
{
    public static class DependencyInjectionExtensions
    {
        public static void AddExpenseTrackingDataAccess(this IServiceCollection services)
        {
            services.AddEntityFrameworkDataAccess();

            services.AddEfCrudRepository<ExpenseCategory, ExpenseTrackingDbContext>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();

            services.AddScoped<IUow<Expense>, EfUow<Expense, ExpenseTrackingDbContext>>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddScoped<IUow<ExpenseRecipient>, EfUow<ExpenseRecipient, ExpenseTrackingDbContext>>();
            services.AddScoped<IExpenseRecipientRepository, ExpenseRecipientRepository>();

            services.AddScoped<IUow<SavingsAccount>, EfUow<SavingsAccount, ExpenseTrackingDbContext>>();
            services.AddScoped<ISavingsAccountRepository, SavingsAccountRepository>();

            services.AddScoped<IUow<SavingsTransaction>, EfUow<SavingsTransaction, ExpenseTrackingDbContext>>();
            services.AddScoped<ISavingsTransactionRepository, SavingsTransactionRepository>();

            services.AddScoped<IUow<Income>, EfUow<Income, ExpenseTrackingDbContext>>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ExpenseTrackingDbContext>(
                    (serviceProvider, options) =>
                    {
                        var configuration = serviceProvider.GetService<ITenantConfiguration>();
                        var connectionString = configuration.GetConnectionString();
                        options.UseSqlServer(connectionString, builder => { builder.EnableRetryOnFailure(3); });
                    });


        }
    }
}
