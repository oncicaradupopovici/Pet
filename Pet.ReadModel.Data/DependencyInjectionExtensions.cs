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
            services.AddEfQuery<Expense, ProjectionsDbContext>();
            services.AddEfQuery<ExpenseByCategory, ProjectionsDbContext>();
            services.AddEfQuery<ExpenseByRecipient, ProjectionsDbContext>();
            services.AddEfQuery<ExpenseMonth, ProjectionsDbContext>();

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
