using System;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Repository.Interfaces;

/// <summary>
///     Epic Repository
/// </summary>
public interface IEpicRepository
{
    /// <summary>
    ///     Get One Epic using its id
    /// </summary>
    /// <param name="id">id of the Epic to retrieve</param>
    /// <returns>The found Epic, null if none</returns>
    public Epic? GetOneById(Guid id);
}