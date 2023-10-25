using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Common.Interfaces;
using Playground.Infrastructure.Persistence;

namespace Playground.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
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
