using Microsoft.Extensions.Options;
using MySpot.Application;
using MySpot.Core;
using MySpot.Infrastructure;
using MySpot.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

var app = builder.Build();

app.UseInfrastructure();

app.MapGet("api", (IOptions<AppOption> options) => Results.Ok(options.Value.Name)); 
app.MapControllers();

app.Run();

