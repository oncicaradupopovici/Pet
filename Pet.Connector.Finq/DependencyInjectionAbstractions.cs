using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pet.Connector.Abstractions;

namespace Pet.Connector.Finq
{
    public static class DependencyInjectionAbstractions
    {
        public static void AddFinqConnector(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConnector, FinqConnector>();
            services.Configure<FinqOptions>(configuration.GetSection("Finq"));
        }
    }
}
