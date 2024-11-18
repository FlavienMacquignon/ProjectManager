using System;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Manager;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.ViewModels;
using ProjectManager_Server.Repository.Interfaces;

namespace ProjectManager_Server.Managers;

/// <summary>
///     Bug Manager, handle Bug Specific logic
/// </summary>
public class BugManager : IBugManager
{
    /// <summary>
    ///     IBugRepository for database abstraction
    /// </summary>
    private readonly IBugRepository _repo;

    /// <summary>
    ///     Bug Manager ctor
    /// </summary>
    /// <param name="repo">IBugRepository for database abstraction</param>
    public BugManager(IBugRepository repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public DescriptionContentViewModel GetOne(Guid id)
    {
        var bug = _repo.GetOne(id);
        NotFoundException<Bug>.ThrowIfNull(bug);
        return bug!.ToDescriptionContentViewModel();
    }

    /// <inheritdoc />
    public DescriptionContentViewModel Add(DescriptionContentViewModel entityToAdd)
    {
        var bug = new Bug(entityToAdd);
        var storedBug = _repo.Add(bug);
        return storedBug.ToDescriptionContentViewModel();
    }
}