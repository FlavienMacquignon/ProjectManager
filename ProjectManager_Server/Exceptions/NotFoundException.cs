using System;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Exceptions;

/// <summary>
/// Not found Exception
/// </summary>
/// <typeparam name="T"></typeparam>
public class NotFoundException<T> : Exception where T : class
{
    /// <summary>
    /// A not found exception
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public NotFoundException(string message) : base(message)
    {

    }

    /// <summary>
    /// Throw a not found exception if the provided param is null
    /// </summary>
    /// <param name="entity">The entity to chek nullability for</param>
    /// <param name="customMessage">Override default message if provided</param>
    public static void ThrowIfNull(T? entity, string customMessage=null!)
    {
        if(entity == null)
        {
            if(customMessage == null)
            customMessage =nameof(T)+" was not found.";
            throw new NotFoundException<T>(customMessage);
        }
    }
}