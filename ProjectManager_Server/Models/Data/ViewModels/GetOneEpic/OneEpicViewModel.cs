using ProjectManager_Server.Models.Data.Entity;
using System;
using System.Collections.Generic;

namespace ProjectManager_Server.Models.Data.ViewModels.GetOneEpic;

/// <summary>
///     An Epic with its representation and associated <see cref="Bug" /> (only title is displayed)
/// </summary>
public record OneEpicViewModel
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
    public required Dictionary<Guid, string>? BugsMinimalDescription { get; set; }
}