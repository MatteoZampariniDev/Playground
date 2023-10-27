using Playground.Application;
using Playground.Infrastructure;
using Playground.Server;
using Playground.Server.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddServer()
    .AddServerUI();

builder.Build()
    .ConfigureServer()
    .Run();
