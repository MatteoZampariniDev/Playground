using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.OtherFeature;
using Playground.Application.Features.Products;
using Playground.Server.UI.Components;

namespace Playground.Server.UI;
public static class DependencyInjection
{
    public static IServiceCollection AddServerUI(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "CleanArchitecture API";

            //// Add the fluent validations schema processor
            //var fluentValidationSchemaProcessor =
            //    sp.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

            //configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);

            //// Add JWT
            //configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            //{
            //    Type = OpenApiSecuritySchemeType.ApiKey,
            //    Name = "Authorization",
            //    In = OpenApiSecurityApiKeyLocation.Header,
            //    Description = "Type into the textbox: Bearer {your JWT token}."
            //});

            //configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }

    public static WebApplication ConfigureServer(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();


        app.MapPost("products/add",
            async (ISender sender) =>
            {
                await sender.Send(new AddProductCommand());

                return Results.NoContent();
            });

        app.MapPost("products/delete",
            async (ISender sender) =>
            {
                await sender.Send(new DeleteProductCommand());

                return Results.NoContent();
            });

        app.MapGet("products/get",
            async (ISender sender) =>
            {
                await sender.Send(new GetProductQuery());

                return Results.NoContent();
            });


        app.MapGet("products/get-all",
            async (ISender sender) =>
            {
                await sender.Send(new GetAllProductsQuery());

                return Results.NoContent();
            });

        app.MapGet("other/get",
            async (ISender sender) =>
            {
                await sender.Send(new GetQuery());

                return Results.NoContent();
            });

        return app;
    }
}
