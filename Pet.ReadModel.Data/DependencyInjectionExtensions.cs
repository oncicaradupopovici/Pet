using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBB.Data.EntityFramework;
using Pet.ReadModel.Projections;
using Pet.Tenant.Abstractions;

namespace Pet.ReadModel.Data
{
    public static class DependencyInjectionExtensions
    {
        public static void AddReadModelDataAccess(this IServiceCollection services)
        {
            services.AddEntityFrameworkDataAccess();
            services.AddEfAsyncEnumerable<Expense, ProjectionsDbContext>();
            services.AddEfAsyncEnumerable<ExpenseByCategory, ProjectionsDbContext>();
            services.AddEfAsyncEnumerable<ExpenseByRecipient, ProjectionsDbContext>();
            services.AddEfAsyncEnumerable<ExpenseMonth, ProjectionsDbContext>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProjectionsDbContext>(
                    (serviceProvider, options) =>
                    {
                        var configuration = serviceProvider.GetService<ITenantConfiguration>();
                        var connectionString = configuration.GetConnectionString();
                        options.UseSqlServer(connectionString, builder => { builder.EnableRetryOnFailure(3); });
                    });


        }
    }
}
