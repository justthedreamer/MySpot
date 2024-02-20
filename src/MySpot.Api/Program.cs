using MySpot.Application;
using MySpot.Application.Services;
using MySpot.Core;
using MySpot.Infrastructure;
using MySpot.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.Services.AddSingleton<IClock, Clock>();

var app = builder.Build();

app.UseInfrastructure();

app.MapControllers();
app.Run();