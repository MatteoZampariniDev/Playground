using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Server.Application.Common.Interfaces;
using Playground.Server.Infrastructure.Persistence;

namespace Playground.Server.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDatabase();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("PlaygroundDb");
            options.EnableSensitiveDataLogging();
        });

        services.AddTransient<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
