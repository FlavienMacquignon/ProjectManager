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
        _db = ContextFactory.CreateDbContext();
    }

    /// <summary>
    ///     The constructed DB Context
    /// </summary>
    /// <value></value>
    private ProjectManagerContext _db { get; set; }

    /// <summary>
    ///     The DB Context Factory
    /// </summary>
    /// <value></value>
    private IDbContextFactory<ProjectManagerContext> ContextFactory { get; }

    /// <inheritdoc />
    public Bug? GetOne(Guid id)
    {
        return _db.Bugs.Include("Description").FirstOrDefault(bug => bug.Id == id);
    }

    /// <inheritdoc />
    public Bug Add(Bug entityToAdd)
    {
        _db.Add(entityToAdd);
        var errorCode = _db.SaveChanges();
        // TODO Throw CustomException ??
        if (errorCode == 0) throw new Exception("Database issue");
        return entityToAdd;
    }

    /// <summary>
    ///     unassign Db Context for sanity
    /// </summary>
    ~BugRepository()
    {
        _db = null!;
    }
}