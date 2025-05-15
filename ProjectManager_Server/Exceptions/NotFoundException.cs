using System;
using System.Collections;

namespace ProjectManager_Server.Exceptions;

/// <summary>
///     Not found Exception
/// </summary>
/// <typeparam name="T">An object</typeparam>
public class NotFoundException<T> : Exception where T : class
{
    /// <summary>
    ///     A not found exception
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <returns>A not found exception</returns>
    private NotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Throw a not found exception if the provided param is null
    /// </summary>
    /// <param name="entity">The entity to check nullability for</param>
    /// <param name="customMessage">Override default message if provided</param>
    public static void ThrowIfNull(T? entity, string? customMessage = null!)
    {
        if (entity != null) return;
        customMessage ??= nameof(T) + " was not found.";
        throw new NotFoundException<T>(customMessage);
    }

    /// <summary>
    ///     Throw a not found exception if the provided param (must be a list) is null or an empty list
    /// </summary>
    /// <param name="entity">The entity to check nullability for</param>
    /// <param name="customMessage">Override default message if provided</param>
    public static void ThrowIfNullOrEmpty(T? entity, string? customMessage = null!)
    {
        if ( entity != null && typeof(IList).IsAssignableFrom(typeof(T)) && ( ( IList )entity ).Count != 0 ) return;
        customMessage ??= nameof(T) + " was not found.";
        throw new NotFoundException<T>(customMessage);
    }
}