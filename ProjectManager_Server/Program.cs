using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectManager_Server.Manager;
using ProjectManager_Server.Repository;
using ProjectManager_Server.Services;

namespace ProjectManager_Server;

/// <summary>
/// Program entry Point
/// </summary>
public class Program
{

    /// <summary>
    /// Maint Method
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddProblemDetails();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.InjectCleanArchitecture();

        var app = builder.Build();

        app.UseDeveloperExceptionPage();
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{StartupInjection.GetAssembly().Version.ToString()}/swagger.json", "My API V1");
            c.RoutePrefix = "swagger";
        }
        );

        app.UseRouting();
        // app.UseHttpsRedirection();

        // app.UseAuthorization();
        app.UseStaticFiles();
        app.UseDeveloperExceptionPage();
        // TODO See how to get rid of that UseEndpoints 
        app.UseEndpoints(endpoints =>
                 {
                     endpoints.MapControllers();
                 });
        app.Run();
    }

}

/// <summary>
/// Static class for DI Injection
/// </summary>
//TODO Document private functions
public static class StartupInjection
{

    /// <summary>
    /// Inject all elements necessary to a clean architecture  
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection InjectCleanArchitecture(this IServiceCollection services)
    {
        services.AddDatabaseContext();
        services.AddSwaggerDoc();
        services.AddRepository();
        services.AddManagers();
        services.AddServices();
        services.AddHostedServices();
        services.AddHealthChecks();
        return services;
    }

    /// <summary>
    /// Retrieve the current assembly for documentation purpose
    /// </summary>
    /// <returns>The running assembly</returns>
    public static AssemblyName GetAssembly()
    {
        AssemblyName currentAssem = Assembly.GetExecutingAssembly().GetName();
        return currentAssem;
    }

    private static IServiceCollection AddDatabaseContext(this IServiceCollection services){
        services.AddDbContextFactory<ProjectManagerContext>(opt =>
        {
            opt.EnableSensitiveDataLogging()
            .UseNpgsql("Host=database; Database=ProjectManager; Username=toto;Password=Toto123*");
        });
        return services;
    }

    /// <summary>
    /// Add Managers that handles Entity specific logic
    /// </summary>
    /// <param name="services"></param>
    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IBugManager, BugManager>();

        return services;

    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IBugRepository, BugRepository>();
        return services;

    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;

    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        return services;

    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        return services;

    }

    private static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        var currentAssem = GetAssembly();
        var version = currentAssem.Version.ToString();
        var name = currentAssem.Name;

        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"{version}", new OpenApiInfo
                    {
                        Version = $"{version}",
                        Title = "ProjectManager API",
                        Description = "A Jira clone in AspNet",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Flavien Macquignon",
                            Email = "[email protected]",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under GPL-3.0",
                            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html#license-text"),
                        }
                    }
                );
                c.EnableAnnotations();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{name}.xml"));
            });
        return services;
    }


}
