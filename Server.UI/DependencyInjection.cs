namespace Playground.Server.UI;
public static class DependencyInjection
{
    public static IServiceCollection AddServerUI(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return services;
    }
}
