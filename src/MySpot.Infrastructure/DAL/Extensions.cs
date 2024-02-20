using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres");
        
        services.AddDbContext<MySpotDbContext>(options => 
                options.UseNpgsql(connectionString)
            )
            .AddScoped<IWeeklyParkingSpotRepository,PostgresWeeklyParkingSpotRepository>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
        
        return services;
    }
}