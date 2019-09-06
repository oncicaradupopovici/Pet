using Microsoft.Extensions.Configuration;
using Pet.Tenant.Abstractions;

namespace Pet.Tenant
{
    public class TenantConfiguration : ITenantConfiguration
    {
        private readonly TenantResolver _tenantResolver;

        public TenantConfiguration(TenantResolver tenantResolver)
        {
            _tenantResolver = tenantResolver;
        }

        public string GetConnectionString()
        {
            var connectionString = _tenantResolver.GetTenantFromContext().ConnectionString;
            return connectionString;
        }
    }
}
