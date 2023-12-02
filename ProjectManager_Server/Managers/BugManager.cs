using ProjectManager_Server.Models;
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
    public Bug GetOne()
    {
        return _repo.GetOne();
    }

    /// <inheritdoc/>
    public Bug Add(Bug entityToAdd){
        return _repo.Add(entityToAdd);
    }
}
