using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
///     Epic Repository
/// </summary>
public class EpicRepository : IEpicRepository
{
    /// <summary>
    ///     ctor
    /// </summary>
    public EpicRepository(IDbContextFactory<ProjectManagerContext> contextFactory)
    {
        ContextFactory = contextFactory;
        Db = ContextFactory.CreateDbContext();
    }

    /// <summary>
    ///     The constructed DB Context
    /// </summary>
    private ProjectManagerContext Db { get; }

    /// <summary>
    ///     The DB Context Factory
    /// </summary>
    private IDbContextFactory<ProjectManagerContext> ContextFactory { get; }

    /// <inheritdoc cref="IEpicRepository.GetOneById" />
    public Epic? GetOneById(Guid id)
    {
        return Db.Epics
            .Include("Description")
            .Include("Bugs")
            .Include("Bugs.Description")
            .FirstOrDefault(epic => epic.Id == id);
    }
}