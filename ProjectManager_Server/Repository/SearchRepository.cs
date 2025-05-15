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

    /// <inheritdoc cref="ISearchRepository.Filter"/>
    public List<Responses> Filter(FilterObject filters)
    {
        var result = SearchAsync(filters).Result;
        var lst = result.Select<SearchResultDTO, Responses>(s => new Responses
        {
            Epics = new EpicViewModel
            {
                Title = s.EpicTitle,
                Content = s.EpicContent,
                BugsMinimalDescription = new Dictionary<Guid, string>
                {
                    {
                        s.BugId ?? throw new Exception(), s.BugContent
                    }
                }
            },
            Bugs = null
        });
        return lst.ToList();
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
    private static IEnumerable<Expression<Func<SearchResultDTO, bool>>> BuildPredicates(FilterObject rules)
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
            from bug in _context.Set<Bug>()
            where epic.ProjectId == projectId || bug.ProjectId == projectId
            select new SearchResultDTO
            {
                EpicId = epic.Id,
                EpicTitle = epic.Description.Title,
                EpicContent = epic.Description.Content,
                BugId = bug.Id,
                BugTitle = bug.Description.Title,
                BugContent = bug.Description.Content
            };
    }
}