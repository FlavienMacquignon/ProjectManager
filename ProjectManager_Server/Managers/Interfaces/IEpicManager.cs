using System;
using ProjectManager_Server.Models.Data.ViewModels.GetOneEpic;

namespace ProjectManager_Server.Managers.Interfaces;

/// <summary>
///     Interface for Epic Manager
/// </summary>
public interface IEpicManager
{
    /// <summary>
    ///     Get One Epic using its id
    /// </summary>
    /// <param name="id">The id of the Epic to search</param>
    /// <returns>An Epic with it's associated Bugs</returns>
    public OneEpicViewModel GetOneById(Guid id);
}