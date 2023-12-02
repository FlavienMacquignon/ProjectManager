using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Models;

namespace ProjectManager_Server.Services
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class ProjectManagerContext : DbContext
    {
        private IConfiguration _config;
        private ILogger<ProjectManagerContext> _logger;

        /// <inheritdoc/>
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options, IConfiguration config, ILogger<ProjectManagerContext> logger)
        : base(options)
        {
            _config = config;
            _logger= logger;
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfig = _config.GetValue<string>("dbConfig");
            var env =_config.GetValue<string>("ASPNETCORE_ENVIRONMENT");
            if(env=="Development"){
                _logger.LogDebug("env is dev, gonna enable DataLogging");
                optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
            }
            optionsBuilder.UseNpgsql(dbConfig);
            base.OnConfiguring(optionsBuilder);
        }

        #region DbSet

        /// <summary>
        /// Bugs DBSet
        /// </summary>
        public DbSet<Bug> Bugs { get; set; }
        #endregion
    }
}