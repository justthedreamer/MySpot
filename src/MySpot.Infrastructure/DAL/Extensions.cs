using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "postgres";

    internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<PostgresOptions>(SectionName);


        services.AddDbContext<MySpotDbContext>(x =>
                x.UseNpgsql(options.ConnectionString)
            )
            .AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>()
            .AddScoped<IUserRepository, PostgresUserRepository>()
            .AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddHostedService<DatabaseInitializer>();
        return services;
    }

}