using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectManager_Server.Models;

namespace ProjectManager_Server.Services
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class ProjectManagerContext : DbContext
    {
        /// <inheritdoc/>
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
        : base(options)
        {

        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO Build connection string from appSettings.json
           // var dbConfig = _config.Get<DataBaseConfig>();
            optionsBuilder.UseNpgsql("Host=database; Database=ProjectManager; Username=toto;Password=Toto123*");
            base.OnConfiguring(optionsBuilder);
        }


        /// <summary>
        /// Bugs DBSet
        /// </summary>
        public DbSet<Bug> Bugs { get; set; }
    }
}