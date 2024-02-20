using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    private const string sectionName = "postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services,IConfiguration configuration)
    {

        var options = GetOptions<PostgresOptions>(configuration,sectionName);
        
        
        services.AddDbContext<MySpotDbContext>(x => 
                x.UseNpgsql(options.ConnectionString)
            )
            .AddScoped<IWeeklyParkingSpotRepository,PostgresWeeklyParkingSpotRepository>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
        
        return services;
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}