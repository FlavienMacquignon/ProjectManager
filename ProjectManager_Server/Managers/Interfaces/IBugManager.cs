using System;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Data.ViewModels.UpBug;

namespace ProjectManager_Server.Managers.Interfaces;

/// <summary>
///     Interface for abstraction of the Bug Manager
/// </summary>
public interface IBugManager
{
    /// <summary>
    ///     Demo GetOne for demonstration
    /// </summary>
    /// <param name="id">The id to search for</param>
    /// <returns>A Bug object</returns>
    public DescriptionContentViewModel GetOne(Guid id);

    /// <summary>
    ///     Demo to add new entity in database
    /// </summary>
    /// <param name="entityToAdd">the entity to add</param>
    /// <returns>The added entity</returns>
    public DescriptionContentViewModel Add(DescriptionContentViewModel entityToAdd);

    /// <summary>
    /// TODO DOC
    /// </summary>
    /// <param name="entityToUpdate"></param>
    /// <returns></returns>
    public UpBugViewModel Update(UpBugViewModel entityToUpdate);
}