using MySpot.Api.Repositories;
using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ReservationsService>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton<IEnumerable<WeeklyParkingSpot>>(serviceProvider =>
{
    var clock = serviceProvider.GetRequiredService<IClock>();
 return new List<WeeklyParkingSpot>()
 {
   new (Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current()), "P1"),
   new (Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current()), "P1"),
   new (Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current()), "P1"),
   new (Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current()), "P1"),
   new (Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current()), "P1"),
 };
});
builder.Services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>();

var app = builder.Build();
app.MapControllers();
app.Run();