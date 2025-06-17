using System;

namespace ProjectManager_Server.Models.Data.ViewModels;

/// <summary>
///     A Bug representation
/// </summary>
public class BugViewModel
{
    /// <summary>
    ///     Id of the Bug
    /// </summary>
    public Guid BugId { get; init; }
    
    /// <summary>
    ///     Bug Title
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    ///     Content (description) filled by the reporter
    /// </summary>
    public string? Content { get; init; }
}