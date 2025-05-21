using ProjectManager_Server.Exceptions;
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

    /// <inheritdoc cref="ISearchManager.Search"/>
    public List<Responses> Search(FilterObject rules)
    {
        var responses = _repository.Filter(rules);
        NotFoundException<List<Responses>>.ThrowIfNullOrEmpty(responses);
        return responses;
    }
}


