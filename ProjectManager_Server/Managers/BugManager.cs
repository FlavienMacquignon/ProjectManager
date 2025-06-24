using System;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Data.ViewModels.UpBug;
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
        return bug.ToDescriptionContentViewModel();
    }

    /// <inheritdoc />
    public DescriptionContentViewModel Add(DescriptionContentViewModel entityToAdd)
    {
        var bug = new Bug(entityToAdd);
        var storedBug = _repo.Add(bug);
        return storedBug.ToDescriptionContentViewModel();
    }

    /// <inheritdoc />
    public UpBugViewModel Update(UpBugViewModel entityToUpdate)
    {
        var bugFound = _repo.GetOne(entityToUpdate.Id);
        NotFoundException<Bug>.ThrowIfNull(bugFound);
        bugFound.Update(entityToUpdate);
        var upBug = _repo.Update(bugFound);
        return upBug.ToUpBugVm();
    }
}