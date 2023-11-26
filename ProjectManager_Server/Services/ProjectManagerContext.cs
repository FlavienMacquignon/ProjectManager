using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models;

namespace ProjectManager_Server.Services
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class ProjectManagerContext : DbContext
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ProjectManagerContext() : base()
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // TODO Build connection string from appSettings.json
            optionsBuilder.UseNpgsql("");
        }


        /// <summary>
        /// 
        /// </summary>
        public DbSet<Bug> Bugs { get; set; }
    }
}