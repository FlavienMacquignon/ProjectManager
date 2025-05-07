using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ProjectManager_Server.Managers;
using ProjectManager_Server.Models.Shared.Internal;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Repository.Extensions;

/// <summary>
/// 
/// </summary>
public static class SearchResultRepositoryExtension
{
    private static readonly ParameterExpression Parameter = Expression.Parameter(typeof(SearchResult), "searchResult");
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicates"></param>
    /// <param name="filter"></param>
    /// <param name="propertyPrefix"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IEnumerable<Expression<Func<SearchResult, bool>>> AddPredicateFromFilter(
        this IEnumerable<Expression<Func<SearchResult, bool>>> predicates, IGenericFilter filter, string propertyPrefix)
    {
        var propertyName = $"{propertyPrefix}{filter.propertyName}";
        var query = filter.query;
        Expression<Func<SearchResult, bool>>? predicate = null;
        if (filter.stringFilter is not null)
            predicate = BuildStringPredicate(propertyName, query, filter.stringFilter);

        if (filter.TimedFilter is not null)
            predicate = BuildDateTimePredicate(propertyName, query, filter.TimedFilter);
        if (filter.BooleanFilter is not null)
            predicate = BuildBooleanPredicate(propertyName, filter.BooleanFilter);
        if (predicate is null)
            throw new InvalidOperationException();
        return predicates.Append(predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="epicFilterBooleanFilter"></param>
    /// <returns></returns>
    private static Expression<Func<SearchResult, bool>> BuildBooleanPredicate(string propertyName,
        BooleanFilter epicFilterBooleanFilter)
    {
        var propertyExpression = Expression.Property(Parameter, propertyName);
        var constant = Expression.Constant(epicFilterBooleanFilter.shouldBeTrue);
        Expression comparaison = Expression.Equal(propertyExpression, constant);
        return Expression.Lambda<Func<SearchResult, bool>>(comparaison, Parameter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTimeProperty"></param>
    /// <param name="epicFilterQuery"></param>
    /// <param name="epicFilterTimedFilter"></param>
    /// <returns></returns>
    private static Expression<Func<SearchResult, bool>> BuildDateTimePredicate(string dateTimeProperty,
        string epicFilterQuery, TimedFilter epicFilterTimedFilter)
    {
        var propertyExpression = Expression.Property(Parameter, dateTimeProperty);
        var constant = Expression.Constant(epicFilterQuery);
        Expression? comparaison;

        if (epicFilterTimedFilter.before == true)
        {
            comparaison = epicFilterTimedFilter.include == true
                ? Expression.LessThanOrEqual(propertyExpression, constant)
                : Expression.LessThan(propertyExpression, constant);
        }
        else if (epicFilterTimedFilter.after == true)
        {
            comparaison = epicFilterTimedFilter.include == true
                ? Expression.GreaterThanOrEqual(propertyExpression, constant)
                : Expression.GreaterThan(propertyExpression, constant);
        }
        else
        {
            comparaison = Expression.Equal(propertyExpression, constant);
        }

        return Expression.Lambda<Func<SearchResult, bool>>(comparaison, Parameter);
    }

    /// <summary>
    ///     Build a search predicate
    /// </summary>
    /// <param name="property">The property used to search in the Entity</param>
    /// <param name="operation">The operation to perform</param>
    /// <param name="value">The value to search</param>
    /// <returns>An expression (a search) to apply to the database</returns>
    /// <exception cref="ArgumentException">If an unauthorised operation is performed in the database</exception>
    private static Expression<Func<SearchResult, bool>> BuildStringPredicate(string property, string operation,
        StringBasedFilter value)
    {
        var propertyExpression = Expression.Property(Parameter, property);
        var constant = Expression.Constant(operation);
        Expression comparison = !value.equals.GetValueOrDefault()
            ? Expression.Call(propertyExpression, "Contains", Type.EmptyTypes, constant)
            : Expression.Equal(propertyExpression, constant);
        // "not" => Expression.Negate(propertyExpression), //TODO TO investigate ==> this is negation
        return Expression.Lambda<Func<SearchResult, bool>>(comparison, Parameter);
    }
}