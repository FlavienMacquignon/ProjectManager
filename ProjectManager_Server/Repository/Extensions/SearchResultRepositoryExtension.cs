using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectManager_Server.Models.Shared.Internal;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Repository.Extensions;

/// <summary>
///     Extension to build Custom Expression Tree to search in the Database for Epic/Bug Description 
/// </summary>
public static class SearchResultRepositoryExtension
{
    private static readonly ParameterExpression Parameter = Expression.Parameter(typeof(SearchResultDTO), "searchResult");

    /// <summary>
    ///     Add a predicate to the expression tree based on the provided Filter
    /// </summary>
    /// <param name="predicates">The list of predicates to add into</param>
    /// <param name="filter">Filter used to build a predicate</param>
    /// <param name="propertyPrefix">The entity to search into, for now either "Epic" or "Bug", see <see cref="SearchRepository.BuildPredicates"/></param>
    /// <exception cref="InvalidOperationException">If no predicate was built for this filter</exception>
    public static void AddPredicateFromFilter(this IList<Expression<Func<SearchResultDTO, bool>>> predicates, GenericFilter filter, string propertyPrefix)
    {
        var propertyName = $"{propertyPrefix}{filter.PropertyName}";
        var query = filter.Query;
        Expression<Func<SearchResultDTO, bool>>? predicate = null;
        if (filter.StringFilter is not null)
            predicate = BuildStringPredicate(propertyName, query, filter.StringFilter);
        if (filter.TimedFilter is not null)
            predicate = BuildDateTimePredicate(propertyName, query, filter.TimedFilter);
        if (filter.BooleanFilter is not null)
            predicate = BuildBooleanPredicate(propertyName, filter.BooleanFilter);
        if (predicate is null)
            throw new InvalidOperationException();
        predicates.Add(predicate);
    }

    /// <summary>
    ///     Build a Boolean predicate based on a BooleanFilter (filter on a boolean property)
    /// </summary>
    /// <param name="propertyName">The property to use to build the predicate (the filtered property)</param>
    /// <param name="booleanFilter">The filter to apply to the property</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildBooleanPredicate(string propertyName,
        BooleanFilter booleanFilter)
    {
        var propertyExpression = Expression.Property(Parameter, propertyName);
        var constant = Expression.Constant(booleanFilter.ShouldBeTrue);
        Expression comparison = Expression.Equal(propertyExpression, constant);
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }

    /// <summary>
    ///     Build a filter based on TimedFilter (filter on a DateTime property)
    /// </summary>
    /// <param name="dateTimeProperty">The property to use to build the predicate (the filtered property)</param>
    /// <param name="query">The user input (The value to compare to the property)</param>
    /// <param name="timedFilter">Values to restrict results (should be before provided date, should be after, does include date with same value ?)</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildDateTimePredicate(string dateTimeProperty,
        string query, TimedFilter timedFilter)
    {
        var propertyExpression = Expression.Property(Parameter, dateTimeProperty);
        var constant = Expression.Constant(query);
        Expression? comparison;

        if (timedFilter.Before == true)
            comparison = timedFilter.Include == true
                ? Expression.LessThanOrEqual(propertyExpression, constant)
                : Expression.LessThan(propertyExpression, constant);
        
        else if (timedFilter.After == true)
            comparison = timedFilter.Include == true
                ? Expression.GreaterThanOrEqual(propertyExpression, constant)
                : Expression.GreaterThan(propertyExpression, constant);
        else
            comparison = Expression.Equal(propertyExpression, constant);
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }

    /// <summary>
    ///     Build a filter based on a StringFilter (filter on a String property)
    /// </summary>
    /// <param name="stringProperty">The property to use to build the predicate (the filtered property)</param>
    /// <param name="query">The user input (The value to compare to the property)</param>
    /// <param name="stringFilter">Value to restrict results (should be strictly equal to query ?)</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildStringPredicate(string stringProperty, string query,
        StringFilter stringFilter)
    {
        var propertyExpression = Expression.Property(Parameter, stringProperty);
        var constant = Expression.Constant(query);
        Expression comparison = !stringFilter.StrictlyEqual.GetValueOrDefault()
            ? Expression.Call(propertyExpression, "Contains", Type.EmptyTypes, constant)
            : Expression.Equal(propertyExpression, constant);
        // "not" => Expression.Negate(propertyExpression), //TODO TO investigate ==> this is negation
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }
}