using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Repository;

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