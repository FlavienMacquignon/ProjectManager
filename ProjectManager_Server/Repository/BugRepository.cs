using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
/// Bug Repository
/// </summary>
public class BugRepository : IBugRepository
{

    /// <summary>
    /// The constructed DB Context
    /// </summary>
    /// <value></value>
    private ProjectManagerContext _db { get; set; }

    /// <summary>
    /// The DB Context Factory
    /// </summary>
    /// <value></value>
    IDbContextFactory<ProjectManagerContext> ContextFactory {get;set;}

    /// <summary>
    /// ctor
    /// </summary>
    public BugRepository(IDbContextFactory<ProjectManagerContext> contextFactory)
    {
        ContextFactory = contextFactory;
        _db = ContextFactory.CreateDbContext();
    }

    /// <inheritdoc/>
    public Bug GetOne()
    {
        return _db.Bugs.First();
    }

    /// <inheritdoc/>
    public Bug Add(Bug entityToAdd){
       _db.Add(entityToAdd);
       int errorCode = _db.SaveChanges();
       if(errorCode== 1) return entityToAdd;
    
        throw new System.Exception("Database issue");
    }

    /// <summary>
    /// unassign Db Context for sanity
    /// </summary>
    ~BugRepository()
    {
        _db = null;
    }
}

/// <summary>
/// Interface for abstraction
/// </summary>
public interface IBugRepository
{
    /// <summary>
    /// Return the first Bug in the Database
    /// </summary>
    /// <returns>A Bug object</returns>
    public Bug GetOne();

    /// <summary>
    /// Add a brand new bug in database
    /// </summary>
    /// <param name="entityToAdd">the entity to add</param>
    /// <returns>The added bug</returns>
    public Bug Add(Bug entityToAdd);
}