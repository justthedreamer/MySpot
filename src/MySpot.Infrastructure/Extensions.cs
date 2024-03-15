using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Infrastructure.Auth;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Logging;
using MySpot.Infrastructure.Security;
using MySpot.Infrastructure.Time;

[assembly: InternalsVisibleTo("MySpot.test.unit")]

namespace MySpot.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddPostgres(configuration);
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSecurity();
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        
        var infrastructureAssembly = typeof(AppOption).Assembly;

        services
            .Scan(s => s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddCustomLogging();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1",new OpenApiInfo()
            {
                Title = "MySpot API",
                Version = "v1"
            });
        });
        services.AddEndpointsApiExplorer();
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.DocumentTitle = "MySpot API";
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}

\ No newline at end of file
