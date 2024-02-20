using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Time;

[assembly:InternalsVisibleTo("MySpot.test.unit")]
namespace MySpot.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddPostgres();
        return services;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        //MIGRATE UPDATE
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MySpotDbContext>();
        
        dbContext.Database.Migrate();

        //SEED
        var weeklyParkingSpots = dbContext.WeeklyParkingSpots.ToList();
        
        if (!weeklyParkingSpots.Any())
        {
            Clock clock = new Clock();

            weeklyParkingSpots =
            [
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()),
                    "P1"),

                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()),
                    "P2"),

                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()),
                    "P3"),

                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()),
                    "P4"),

                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()),
                    "P5")
                
            ];

            dbContext.AddRange(weeklyParkingSpots);
            dbContext.SaveChanges();
        }
        
        return app;
    }
}