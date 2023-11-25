using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

        AssemblyName currentAssem = Assembly.GetExecutingAssembly().GetName();
        var name = currentAssem.Name;
        var version = currentAssem.Version;
        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
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
                var xmlFilename = $"{name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        var app = builder.Build();

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{version}/swagger.json", "My API V1");
            c.RoutePrefix = "swagger";            
        }
        );

        app.UseRouting();
        // app.UseHttpsRedirection();

        // app.UseAuthorization();
        app.UseStaticFiles();
        app.UseDeveloperExceptionPage();
        app.UseEndpoints(endpoints =>
                 {
                     endpoints.MapControllers();
                 });
        app.Run();
    }
}
