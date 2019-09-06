namespace Pet.Tenant.Abstractions
{
    public interface ITenantConfiguration
    {
        string GetConnectionString();
    }
}
