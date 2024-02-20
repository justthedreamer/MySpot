using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Time;

[assembly:InternalsVisibleTo("MySpot.test.unit")]
namespace MySpot.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        return services;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        return app;
    }
}