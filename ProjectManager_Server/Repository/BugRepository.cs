using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Repository.Abstract;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
///     Bug Repository
/// </summary>
public class BugRepository : AbstractRepository, IBugRepository
{
    /// <summary>
    ///     ctor call base one
    /// </summary>
    public BugRepository(IDbContextFactory<ProjectManagerContext> contextFactory) : base(contextFactory.CreateDbContext())
    { }

    /// <inheritdoc />
    public Bug? GetOne(Guid id)
    {
        return ((ProjectManagerContext)Db).Bugs.Include("Description").SingleOrDefault(bug => bug.Id == id);
    }

    /// <inheritdoc />
    public Bug Add(Bug entityToAdd)
    {
        Db.Add(entityToAdd);
        SaveChanges();
        return entityToAdd;
    }

    /// <inheritdoc />
    public Bug Update(Bug bugToUpdate)
    {
        Db.Update(bugToUpdate);
        SaveChanges();
        return bugToUpdate;
    }
}