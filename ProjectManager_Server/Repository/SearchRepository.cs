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
///     A search Repository to search through Bugs and Epic using a very inefficient way to do it
/// </summary>
/// <param name="context"></param>
public class SearchRepository(ProjectManagerContext context) : ISearchRepository // TODO TO Test
{
    private readonly DbContext _context = context;

    /// <inheritdoc />
    public List<Responses> Filter(FilterObject filters)
    {
        var result = SearchAsync(filters).Result;
        var lst = result.Select<SearchResult, Responses>(s => new Responses
        {
            Epics = new EpicViewModel
            {
                Title = s.EpicTitle,
                Content = s.EpicContent,
                BugsMinimalDescription = new Dictionary<Guid, string>
                {
                    {(Guid)s.BugId,s.BugContent} 
                },

            },
            Bugs = null
        });
        return lst.ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    private async Task<IEnumerable<SearchResult>> SearchAsync(FilterObject filters)
    {
        // Sub-setting Epic and Bug for selected ProjectId
        var subSetSearchResult = GetSubSet(filters.projectId);
        var predicates = BuildPredicates(filters); // Building search predicates using query
        subSetSearchResult =
            predicates.Aggregate(subSetSearchResult,
                (current, predicate) => current.Where(predicate)); // Applying Search Predicates to the subset

        return await subSetSearchResult.ToListAsync();
    }

    /// <summary>
    ///     Build a list of predicates
    /// </summary>
    /// <param name="rules">The set of rules tu use to search for this predicate</param>
    /// <returns>Return the list of predicates to apply</returns>
    private static IEnumerable<Expression<Func<SearchResult, bool>>> BuildPredicates(FilterObject rules)
    {
        IEnumerable<Expression<Func<SearchResult, bool>>> predicates = new List<Expression<Func<SearchResult, bool>>>();
        if (rules.epicFilters != null)
            foreach (var epicFilter in rules.epicFilters)
                predicates.AddPredicateFromFilter(epicFilter, "Epic");
        if (rules.bugFilters != null)
            foreach (var bugFilter in rules.bugFilters)
                predicates.AddPredicateFromFilter(bugFilter, "Bug");
        return predicates;
    }


    /// <summary>
    ///     Transform data set into a searchable set of SearchResult
    /// </summary>
    /// <param name="projectId">The project to search into</param>
    /// <returns>A subset of SearchResult</returns>
    private IQueryable<SearchResult> GetSubSet(Guid projectId)
    {
        return
            from epic in _context.Set<Epic>()
            from bug in _context.Set<Bug>()
            where epic.ProjectId == projectId || bug.ProjectId == projectId
            select new SearchResult
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