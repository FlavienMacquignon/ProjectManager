using System.Collections.Generic;
using ProjectManager_Server.Managers;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Repository.Interfaces;

/// <summary>
///     Interface for Search Repository
/// </summary>
public interface ISearchRepository
{
    /// <summary>
    ///     Filter Epics and Bugs based on rules provided by filter object
    /// </summary>
    /// <param name="rules">The set of filter to use</param>
    /// <returns>A list of responses that match the provided set of rules</returns>
    public List<Responses> Filter(FilterObject rules);
}