using System;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.ViewModels;
using ProjectManager_Server.Repository;

namespace ProjectManager_Server.Manager;

/// <summary>
/// Bug Manager, handle Bug Specific logic
/// </summary>
public class BugManager : IBugManager
{
    /// <summary>
    /// IBugRepository for database abstraction
    /// </summary>
    private IBugRepository _repo;

    /// <summary>
    /// Bug Manager ctor
    /// </summary>
    /// <param name="repo">IBugRepository for database abstraction</param>
    public BugManager(IBugRepository repo)
    {
        _repo = repo;
    }

    /// <inheritdoc/>
    public DescriptionContentViewModel GetOne(Guid id)
    {
        var bug =_repo.GetOne(id);
        NotFoundException<Bug>.ThrowIfNull(bug);
        return new DescriptionContentViewModel(){Title=bug!.Description.Title, Content=bug.Description.Content};
    }

    /// <inheritdoc/>
    public Bug Add(DescriptionContentViewModel entityToAdd){
        var bug = new Bug(entityToAdd);
        return _repo.Add(bug);
    }
}
