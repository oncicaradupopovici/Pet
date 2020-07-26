using Microsoft.Extensions.DependencyInjection;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.IncomeAggregate;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.ExpenseTracking.Domain
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ExpenseService>();
            services.AddScoped<ExpenseRecipientService>();
            services.AddScoped<ExpenseSettingsService>();
            services.AddScoped<ExpenseMonthService>();
            services.AddScoped<SavingsService>();
            services.AddScoped<IncomeService>();

            services.AddScoped<ExpenseFactory>();
            services.AddScoped<SavingsTransactionFactory>();
            services.AddScoped<IncomeFactory>();
            
        }
    }
}
