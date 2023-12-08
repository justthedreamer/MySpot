using Microsoft.EntityFrameworkCore;
using MySpot.Application;
using MySpot.Core;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Time;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddControllers();

var app = builder.Build();

app.UseInfrastructure();

app.Run();
