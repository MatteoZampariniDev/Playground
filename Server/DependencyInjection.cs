using Microsoft.Extensions.DependencyInjection;

namespace Playground.Server;
public static class DependencyInjection
{
    public static IServiceCollection AddServer(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
