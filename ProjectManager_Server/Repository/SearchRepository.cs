using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal;
using ProjectManager_Server.Models.Shared.Internal.Filter;
using ProjectManager_Server.Repository.Extensions;
using ProjectManager_Server.Repository.Interfaces;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
///     A search Repository to search through Bugs and Epic
/// </summary>
/// <param name="context">Database Context</param>
public class SearchRepository(ProjectManagerContext context) : ISearchRepository // TODO TO Test
{
    private readonly DbContext _context = context;

    /// <inheritdoc cref="ISearchRepository.Filter(FilterObject)"/>
    public List<Responses> Filter(FilterObject filters)
    {
        var result = SearchAsync(filters).Result;
        var lst = SearchResultDTOsToResponses(result);
        return lst.ToList();
    }

    /// <summary>
    ///     Transform a List of SearchResultDTO into a List of Responses
    /// </summary>
    /// <param name="searchResults">The list to transform</param>
    /// <returns>The transformed list</returns>
    private static IEnumerable<Responses> SearchResultDTOsToResponses(IEnumerable<SearchResultDTO> searchResults)
    {
        var dicEpVm = new Dictionary<Guid, EpicViewModel>();
        var repLst = new List<Responses>();
        foreach ( SearchResultDTO sr in searchResults )
        {
            if ( sr.EpicId is not null )
            {
                if ( !dicEpVm.ContainsKey(( Guid )sr.EpicId) )
                {
                    dicEpVm.Add(( Guid )sr.EpicId, new EpicViewModel
                    {
                        Title = sr.EpicTitle ?? string.Empty, 
                        Content = sr.EpicContent,
                        BugsMinimalDescription = new Dictionary<Guid, string>()
                    });
                    if ( sr.BugId is not null )
                        dicEpVm[( Guid )sr.EpicId].BugsMinimalDescription?.Add(( Guid )sr.BugId!, sr.BugTitle ?? string.Empty);
                }
                else
                    dicEpVm[( Guid )sr.EpicId].BugsMinimalDescription?.Add(( Guid )sr.BugId!, sr.BugTitle ?? string.Empty);

                if ( dicEpVm[( Guid )sr.EpicId].BugsMinimalDescription != null && dicEpVm[( Guid )sr.EpicId].BugsMinimalDescription?.Count == 0 )
                    dicEpVm[( Guid )sr.EpicId].BugsMinimalDescription = null;
            }
            else if ( sr.BugId is not null && sr.EpicId is null ) 
                repLst.Add(new Responses
                {
                    Bugs = new BugViewModel
                    {
                        BugId = ( Guid )sr.BugId, 
                        Title = sr.BugTitle ?? string.Empty, 
                        Content = sr.BugContent
                    }
                });
        }

        repLst.AddRange(dicEpVm.Select(ep => new Responses
        {
            Epics = ep.Value
        }));

        return repLst;
    }

    /// <summary>
    ///     This function act as a brain for search:
    ///     <ul>
    ///         <li>It build a list of predicates to narrow down the results based on provided filters</li>
    ///         <li>It subset the query based on the projectId</li>
    ///         <li>Apply the list of the predicates to the DbSet</li>
    ///         <li>Send the Freshly build Expression Tree to the Database (after Entity Framework conversion to SQL)</li>
    ///     </ul> 
    /// </summary>
    /// <param name="filters">The list of filters to apply</param>
    /// <returns>An IEnumerable of Search Results containing result of filter</returns>
    private async Task<IEnumerable<SearchResultDTO>> SearchAsync(FilterObject filters)
    {
        var predicates = BuildPredicates(filters); // Building search predicates using query
        var subSetSearchResult = GetSubSetSearchResultForProject(filters.ProjectId); // Sub-setting Epic and Bug for selected ProjectId
        subSetSearchResult = predicates.Aggregate(subSetSearchResult, (current, predicate) => current.Where(predicate)); // Applying Search Predicates to the subset
        return await subSetSearchResult.ToListAsync();
    }

    /// <summary>
    ///     Build a list of predicates
    /// </summary>
    /// <param name="rules">The set of rules tu use to search for this predicate</param>
    /// <returns>Return the list of predicates to apply</returns>
    private static List<Expression<Func<SearchResultDTO, bool>>> BuildPredicates(FilterObject rules)
    {
        const string epic = "Epic";
        const string bug = "Bug";
        List<Expression<Func<SearchResultDTO, bool>>> predicates = [];
        if ( rules.EpicFilters != null )
            foreach ( var epicFilter in rules.EpicFilters )
                predicates.AddPredicateFromFilter(epicFilter, epic);

        if ( rules.BugFilters != null )
            foreach ( var bugFilter in rules.BugFilters )
                predicates.AddPredicateFromFilter(bugFilter, bug);

        return predicates;
    }

    /// <summary>
    ///     Transform data set into a searchable set of SearchResult
    /// </summary>
    /// <param name="projectId">The project to search into</param>
    /// <returns>A subset of SearchResult</returns>
    private IQueryable<SearchResultDTO> GetSubSetSearchResultForProject(Guid projectId)
    {
        return
            from epic in _context.Set<Epic>()
            join bug in _context.Set<Bug>() on epic.Id equals bug.EpicId into EpicBug
            from bug in EpicBug.DefaultIfEmpty()
            where epic.ProjectId == projectId || bug.ProjectId == projectId
            select new SearchResultDTO
            {
                EpicId = epic.Id,
                EpicTitle = epic.Description.Title,
                EpicContent = epic.Description.Content,
                BugId = bug.Id,
                BugTitle = bug.Description.Title,
                BugContent = bug.Description.Content,
                BugCreatedAt = bug.CreatedAt
            };
    }
}