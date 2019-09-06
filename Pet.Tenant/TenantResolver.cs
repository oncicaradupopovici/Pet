using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Pet.Tenant
{
    public class TenantResolver
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantResolver(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Tenant GetTenantFromContext()
        {
            var tenantId = GetTenantIdFromContext();
            if (tenantId.HasValue)
            {
                var tenantConnectionString = _configuration.GetConnectionString($"Tenant_{tenantId.Value}");
                return new Tenant(tenantId.Value, tenantConnectionString);
            }

            return GetDefaultTenant();
        }

        private Tenant GetDefaultTenant()
        {
            return new Tenant(Guid.Empty, _configuration.GetConnectionString("Pet_Database"));
        }

        private Guid? GetTenantIdFromContext()
        {
            var context = _httpContextAccessor.HttpContext;
            if(Guid.TryParse(context.Request.Headers["TenantId"], out Guid tenantId)){
                return tenantId;
            }

            return null;
        }
    }
}
