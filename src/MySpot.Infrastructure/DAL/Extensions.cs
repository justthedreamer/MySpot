using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string connectionString = "Host=localhost;Database=MySpot;Username=postgres;Password=";
        
        services.AddDbContext<MySpotDbContext>(options => 
                options.UseNpgsql(connectionString)
            )
            .AddScoped<IWeeklyParkingSpotRepository,PostgresWeeklyParkingSpotRepository>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
        
        return services;
    }
}