using MySpot.Api.Repositories;
using MySpot.Application;
using MySpot.Application.Services;
using MySpot.Core;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();

builder.Services.AddSingleton<IClock, Clock>();

var app = builder.Build();
app.MapControllers();
app.Run();