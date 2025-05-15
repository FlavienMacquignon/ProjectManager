using System;
using System.Collections.Generic;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Models.Data.ViewModels;

/// <summary>
///     An Epic with its representation and associated <see cref="Bug" /> (only title is displayed)
/// </summary>
public record EpicViewModel
{
    /// <summary>
    ///     Epic Title
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    ///     Content (description) filled by the reporter
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    ///     Couple <see cref="Bug.Id" /> and <see cref="Description.Title" /> for this <see cref="Bug" />
    /// </summary>
    public required Dictionary<Guid, string> BugsMinimalDescription { get; init; }
}