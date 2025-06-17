using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Services;

/// <summary>
///     Database Context
/// </summary>
public class ProjectManagerContext : DbContext
{
    private readonly IConfiguration _config;
    private readonly ILogger<ProjectManagerContext> _logger;

    /// <inheritdoc />
    public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options, IConfiguration config,
        ILogger<ProjectManagerContext> logger)
        : base(options)
    {
        _config = config;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbConfig = _config.GetValue<string>("dbConfig");
        var env = _config.GetValue<string>("ASPNETCORE_ENVIRONMENT");
        optionsBuilder.UseNpgsql(dbConfig, optB => optB.MigrationsHistoryTable("history", "migration"));
        if ( env == "Development" )
        {
            _logger.LogDebug("env is dev, gonna enable DataLogging");
            optionsBuilder.LogTo(Console.WriteLine, (id, level) => id == Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted && level >= LogLevel.Debug)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        
        base.OnConfiguring(optionsBuilder);
    }

    #region DbSet

    /// <summary>
    ///     Epic DBSet
    /// </summary>
    public DbSet<Epic> Epics { get; set; }

    /// <summary>
    ///     Project DBSet
    /// </summary>
    public DbSet<Project> Project { get; set; }

    /// <summary>
    ///     Bugs DBSet
    /// </summary>
    public DbSet<Bug> Bugs { get; set; }

    #endregion
}