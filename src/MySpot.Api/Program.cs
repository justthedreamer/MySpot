using MySpot.Application;
using MySpot.Application.Services;
using MySpot.Core;
using MySpot.Infrastructure;
using MySpot.Infrastructure.Logging;
using MySpot.Infrastructure.Time;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.UseSerilog();

var app = builder.Build();

app.UseInfrastructure();

app.MapControllers();

app.Run();