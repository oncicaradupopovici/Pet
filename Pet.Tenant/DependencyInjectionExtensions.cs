using Microsoft.Extensions.DependencyInjection;
using Pet.Tenant.Abstractions;

namespace Pet.Tenant
{
    public static class DependencyInjectionExtensions
    {
        public static void AddPetTenant(this IServiceCollection services)
        {
            services.AddSingleton<ITenantConfiguration, TenantConfiguration>();
            services.AddSingleton<TenantResolver>();

        }
    }
}
