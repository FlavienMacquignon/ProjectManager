using ProjectManager_Server.Attribute;
using System;
using System.Collections.Generic;

namespace ProjectManager_Server.Models.Shared.Internal.Filter;

/// <summary>
///     An Object to provide filters on Bugs an Epics
/// </summary>
public record FilterObject
{
    /// <summary>
    ///     The project to search into
    /// </summary>
    public Guid ProjectId { get; set; }
    
    /// <summary>
    ///     List of filters to apply on Epics
    /// </summary>
    [NumberOfNullAuthorized([nameof(BugFilters)],1,1)]
    public List<GenericFilter>? EpicFilters { get; set; }
    
    /// <summary>
    ///     List of filters to apply on Bugs
    /// </summary>
    public List<GenericFilter>? BugFilters { get; set; }
}
