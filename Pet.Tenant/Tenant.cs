using System;

namespace Pet.Tenant
{
    public class Tenant
    {
        public Guid TenantId { get; }
        public string ConnectionString { get; }

        public Tenant(Guid tenantId, string connectionString)
        {
            TenantId = tenantId;
            ConnectionString = connectionString;
        }
    }
}
