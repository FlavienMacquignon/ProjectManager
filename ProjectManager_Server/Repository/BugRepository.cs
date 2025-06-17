using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
///     Bug Repository
/// </summary>
public class BugRepository : IBugRepository
{
    /// <summary>
    ///     ctor
    /// </summary>
    public BugRepository(IDbContextFactory<ProjectManagerContext> contextFactory)
    {
        ContextFactory = contextFactory;
        Db = ContextFactory.CreateDbContext();
    }

    /// <summary>
    ///     The constructed DB Context
    /// </summary>
    private ProjectManagerContext Db { get; set; }

    /// <summary>
    ///     The DB Context Factory
    /// </summary>
    private IDbContextFactory<ProjectManagerContext> ContextFactory { get; }

    /// <inheritdoc />
    public Bug? GetOne(Guid id)
    {
        return Db.Bugs.Include("Description").FirstOrDefault(bug => bug.Id == id);
    }

    /// <inheritdoc />
    public Bug Add(Bug entityToAdd)
    {
        Db.Add(entityToAdd);
        var errorCode = Db.SaveChanges();
        // TODO Throw CustomException ??
        if (errorCode == 0) throw new Exception("Database issue");
        return entityToAdd;
    }

    /// <summary>
    ///     unassign Db Context for sanity
    /// </summary>
    ~BugRepository()
    {
        Db = null!;
    }
}