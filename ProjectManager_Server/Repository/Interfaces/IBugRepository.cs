using System;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Repository.Interfaces;

/// <summary>
///     Interface for abstraction
/// </summary>
public interface IBugRepository
{
    /// <summary>
    ///     Return the first Bug in the Database
    /// </summary>
    /// <param name="id">The id to search for</param>
    /// <returns>A Bug object</returns>
    public Bug? GetOne(Guid id);

    /// <summary>
    ///     Add a brand new bug in database
    /// </summary>
    /// <param name="entityToAdd">The entity to add</param>
    /// <returns>The added bug</returns>
    public Bug Add(Bug entityToAdd);
}