using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

// TODO DOC
namespace ProjectManager_Server.Repository;

public class EpicRepository : IEpicRepository
{
    /// <summary>
    ///     ctor
    /// </summary>
    public EpicRepository(IDbContextFactory<ProjectManagerContext> contextFactory)
    {
        ContextFactory = contextFactory;
        _db = ContextFactory.CreateDbContext();
    }

    /// <summary>
    ///     The constructed DB Context
    /// </summary>
    /// <value></value>
    private ProjectManagerContext _db { get; }

    /// <summary>
    ///     The DB Context Factory
    /// </summary>
    /// <value></value>
    private IDbContextFactory<ProjectManagerContext> ContextFactory { get; }

    public Epic? GetOneById(Guid id)
    {
        return _db.Epics
            .Include("Description")
            .Include("Bugs")
            .Include("Bugs.Description")
            .FirstOrDefault(epic => epic.Id == id);
    }
}