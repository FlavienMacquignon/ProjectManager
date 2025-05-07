using System.Collections.Generic;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal.Filter;
using ProjectManager_Server.Repository.Interfaces;

namespace ProjectManager_Server.Managers;

/// <summary>
///     Search Manager
/// </summary>
public class SearchManager : ISearchManager
{
    private readonly ISearchRepository _repository;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="searchRepository">Search Repository</param>
    public SearchManager(ISearchRepository searchRepository)
    {
        _repository = searchRepository;
    }

    /// <inheritdoc />
    public List<Responses>? Filter(FilterObject rules)
    {
        // TODO Validation ==> What can be active at the same time ? ==> To Test
        return _repository.Filter(rules);
        // TODO Transform Data into a set of Responses here
    }
}


