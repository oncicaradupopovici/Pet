using Microsoft.Extensions.DependencyInjection;
using Pet.Connector.Abstractions;

namespace Pet.Connector.Ing
{
    public static class DependencyInjectionAbstractions
    {
        public static void AddIngConnector(this IServiceCollection services)
        {
            services.AddTransient<IConnector, IngConnector>();
        }
    }
}
