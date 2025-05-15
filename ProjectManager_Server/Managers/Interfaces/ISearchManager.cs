using ProjectManager_Server.Exceptions;
using System.Collections.Generic;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Managers.Interfaces;

/// <summary>
///     Interface for Search Manager
/// </summary>
public interface ISearchManager
{
    /// <summary>
    ///     Filter the database using the set of rules provided by the user
    /// </summary>
    /// <param name="rules">The set of rules </param>
    /// <returns>A list of responses if any is found</returns>
    /// <exception cref="NotFoundException{T}">If Responses is not found</exception>
    List<Responses> Filter(FilterObject rules);
}