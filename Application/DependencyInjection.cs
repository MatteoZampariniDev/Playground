using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Playground.Application.Common.Caching;
using Playground.Application.Common.Interfaces;
using Playground.Application.Pipeline;
using System.Reflection;

namespace Playground.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
            config.AddOpenBehavior(typeof(MemoryCacheBehaviour<,>));
        });

        services.AddLazyCache();
        services.AddScoped<IRequestCacheService, RequestCacheService>();
        services.AddRequestCaches();

        return services;
    }

    private static IServiceCollection AddRequestCaches(this IServiceCollection services)
    {
        var cacheTypes = Assembly.GetExecutingAssembly()
            .GetTypes().Where(type => type.IsClass
            && !type.IsAbstract
            && type.IsSubclassOf(typeof(BaseCache)));

        foreach (var t in cacheTypes)
        {
            services.TryAddSingleton(t);
        }

        return services;
    }
}