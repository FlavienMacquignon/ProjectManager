using ProjectManager_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectManager_Server.Models.Shared.Internal;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Repository.Extensions;

/// <summary>
///     Extension to build Custom Expression Tree, used to search in the Database for Epic/Bug Description 
/// </summary>
public static class SearchResultRepositoryExtension
{
    private static readonly ParameterExpression Parameter = Expression.Parameter(typeof(SearchResultDTO), nameof(SearchResultDTO));

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
        if ( filter.StringFilter is not null )
            predicate = BuildStringPredicate(propertyName, query, filter.StringFilter, filter.Negated);
        if ( filter.TimedFilter is not null )
            predicate = BuildDateTimePredicate(propertyName, query, filter.TimedFilter, filter.Negated);
        if ( filter.BooleanFilter is not null )
            predicate = BuildBooleanPredicate(propertyName, filter.BooleanFilter, filter.Negated);
        if ( predicate is null )
            throw new InvalidOperationException();
        predicates.Add(predicate);
    }

    /// <summary>
    ///     Build a Boolean predicate based on a BooleanFilter (filter on a boolean property)
    /// </summary>
    /// <param name="propertyName">The property to use to build the predicate (the filtered property)</param>
    /// <param name="booleanFilter">The filter to apply to the property</param>
    /// <param name="negated">Should the Expression be negated</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildBooleanPredicate(string propertyName,
        BooleanFilter booleanFilter, bool negated)
    {
        var propertyExpression = Expression.Property(Parameter, propertyName);
        var constant = Expression.Constant(booleanFilter.ShouldBeTrue);
        Expression comparison = Expression.Equal(propertyExpression, constant);
        comparison = comparison.Negate(negated);
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }

    /// <summary>
    ///     Build a filter based on TimedFilter (filter on a DateTime property)
    /// </summary>
    /// <param name="dateTimeProperty">The property to use to build the predicate (the filtered property)</param>
    /// <param name="query">The user input (The value to compare to the property)</param>
    /// <param name="timedFilter">Values to restrict results (should be before provided date, should be after, does include date with same value ?)</param>
    /// <param name="negated">Should the Expression be negated</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildDateTimePredicate(string dateTimeProperty,
        string query, TimedFilter timedFilter, bool negated)
    {
        var propertyExpression = Expression.Property(Parameter, dateTimeProperty);
        var queryDt = query.ToUniversalDateTime();
        var constant = Expression.Constant(queryDt, typeof(DateTime?));
        Expression? comparison;

        if ( timedFilter.Before == true )
            comparison = timedFilter.Include == true
                ? Expression.LessThanOrEqual(propertyExpression, constant)
                : Expression.LessThan(propertyExpression, constant);

        else if ( timedFilter.After == true )
            comparison = timedFilter.Include == true
                ? Expression.GreaterThanOrEqual(propertyExpression, constant)
                : Expression.GreaterThan(propertyExpression, constant);
        else
        {
            ArgumentNullException.ThrowIfNull(queryDt);
            var startOfDay = queryDt.Value.Date;
            var nextDay = startOfDay.AddDays(1);
            var greaterThanOrEqual = Expression.GreaterThanOrEqual(propertyExpression, Expression.Constant(startOfDay, typeof(DateTime?)));
            var lessThan = Expression.LessThan(propertyExpression, Expression.Constant(nextDay, typeof(DateTime?)));
            comparison = Expression.AndAlso(greaterThanOrEqual, lessThan);
        }

        comparison = comparison.Negate(negated);
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }

    /// <summary>
    ///     Build a filter based on a StringFilter (filter on a String property)
    /// </summary>
    /// <param name="stringProperty">The property to use to build the predicate (the filtered property)</param>
    /// <param name="query">The user input (The value to compare to the property)</param>
    /// <param name="stringFilter">Value to restrict results (should be strictly equal to query ?)</param>
    /// <param name="negated">Should the Expression be negated</param>
    /// <returns>A predicate to add to the list of predicate</returns>
    private static Expression<Func<SearchResultDTO, bool>> BuildStringPredicate(string stringProperty, string query,
        StringFilter stringFilter, bool negated)
    {
        var propertyExpression = Expression.Property(Parameter, stringProperty);
        var constant = Expression.Constant(query);
        Expression comparison = !stringFilter.StrictlyEqual.GetValueOrDefault()
            ? Expression.Call(propertyExpression, "Contains", Type.EmptyTypes, constant)
            : Expression.Equal(propertyExpression, constant);
        comparison = comparison.Negate(negated);
        return Expression.Lambda<Func<SearchResultDTO, bool>>(comparison, Parameter);
    }

    /// <summary>
    ///     Negate an Expression if necessary
    /// </summary>
    /// <param name="expression">The expression to negate (if <see cref="negated"/> is true)</param>
    /// <param name="negated">If true, negate the provided expression, else do nothing</param>
    /// <returns>The provided expression (can be negated)</returns>
    private static Expression Negate(this Expression expression, bool negated)
    {
        if ( negated )
            expression = Expression.Not(expression);
        return expression;
    }
}