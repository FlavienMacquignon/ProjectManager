using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectManager_Server.Managers;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Repository;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

namespace ProjectManager_Server;

/// <summary>
///     Program entry Point
/// </summary>
public static class Program
{
    /// <summary>
    ///     Maint Method
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        if (builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
            builder.Services.AddProblemDetails();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.InjectCleanArchitecture(builder.Configuration);

        var app = builder.Build();

        if (builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{StartupInjection.GetAssembly().Version}/swagger.json", "My API V1");
                    c.RoutePrefix = "swagger";
                }
            );
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        // app.UseHttpsRedirection();

        // app.UseAuthorization();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.Run();
    }
}

/// <summary>
///     Static class for DI Injection
/// </summary>
public static class StartupInjection
{
    /// <summary>
    ///     Inject all elements necessary to a clean architecture
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">The IConfiguration of the application, can be used to configure swagger</param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection InjectCleanArchitecture(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services
            .AddDatabaseContext()
            .AddSwaggerDoc()
            .AddRepository()
            .AddManagers()
            .AddServices()
            .AddHostedServices()
            .AddHealthChecks();
    }

    /// <summary>
    ///     Retrieve the current assembly for documentation purpose
    /// </summary>
    /// <returns>The running assembly</returns>
    public static AssemblyName GetAssembly()
    {
        var currentAssem = Assembly.GetExecutingAssembly().GetName();
        return currentAssem;
    }

    /// <summary>
    ///     Add a Db Context Factory to the <see cref="IServiceCollection" />
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContextFactory<ProjectManagerContext>();
        return services;
    }

    /// <summary>
    ///     Add Managers that handles Entity specific logic
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IBugManager, BugManager>();
        services.AddScoped<IEpicManager, EpicManager>();

        services.AddScoped<ISearchManager, SearchManager>();

        return services;
    }

    /// <summary>
    ///     Add Repository that handles database abstractions
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IBugRepository, BugRepository>();
        services.AddScoped<IEpicRepository, EpicRepository>();

        services.AddScoped<ISearchRepository, SearchRepository>();

        return services;
    }

    /// <summary>
    ///     Add a set of custom services
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    ///     Add a set of hosted services
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    ///     Add a list of custom healtchecks endpoints to ensure disponibility of the app
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    ///     Add a swagger definition for this application
    /// </summary>
    /// <param name="services">The collection of services</param>
    /// <returns>The collection of Services for further chaining</returns>
    private static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        var currentAssem = GetAssembly();
        var version = currentAssem.Version;
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
                        Email = "flavien.macquignon@fastmail.fr",
                        Url = new Uri("https://flavienmacquignon.github.io/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GPL-3.0",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html#license-text")
                    }
                }
            );
            c.EnableAnnotations();
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{name}.xml"));
        });
        return services;
    }
}