using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBB.Data.EntityFramework;
using Pet.OpenBanking.Data.Repositories;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate;
using Pet.Tenant.Abstractions;

namespace Pet.OpenBanking.Data
{
    public static class DependencyInjectionExtensions
    {
        public static void AddOpenBankingDataAccess(this IServiceCollection services)
        {
            services.AddEntityFrameworkDataAccess();

            services.AddEfCrudRepository<OpenBankingPayment, OpenBankingDbContext>();
            services.AddScoped<IOpenBankingPaymentRepository, OpenBankingPaymentRepository>();


            services.AddEntityFrameworkSqlServer()
                .AddDbContext<OpenBankingDbContext>(
                    (serviceProvider, options) =>
                    {
                        var configuration = serviceProvider.GetService<ITenantConfiguration>();
                        var connectionString = configuration.GetConnectionString();
                        options.UseSqlServer(connectionString, builder => { builder.EnableRetryOnFailure(3); });
                    });


        }
    }
}
