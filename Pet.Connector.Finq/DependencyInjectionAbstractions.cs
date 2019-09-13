using Microsoft.Extensions.DependencyInjection;
using Pet.Connector.Abstractions;

namespace Pet.Connector.Finq
{
    public static class DependencyInjectionAbstractions
    {
        public static void AddFinqConnector(this IServiceCollection services)
        {
            services.AddTransient<IConnector, FinqConnector>();
        }
    }
}
