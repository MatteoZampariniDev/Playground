using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Playground.Server.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //config.NotificationPublisher = new ParallelNoWaitPublisher();
            //config.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));
            //config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            //config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            //config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
            //config.AddOpenBehavior(typeof(MemoryCacheBehaviour<,>));
            //config.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            //config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
        });

        //services.AddLazyCache();

        return services;
    }

}