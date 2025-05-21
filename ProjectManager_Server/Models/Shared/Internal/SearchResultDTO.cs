using System;

namespace ProjectManager_Server.Models.Shared.Internal;

/// <summary>
///     A DTO to search in Epics and Bug of this Database
/// </summary>
public record SearchResultDTO
{
    /// <summary>
    ///     Id of the returned Epic
    /// </summary>
    public Guid? EpicId { get; set; }
    /// <summary>
    ///     Title of the returned Epic
    /// </summary>
    public string? EpicTitle { get; set; }
    /// <summary>
    ///     Content of the returned Epic
    /// </summary>
    public string? EpicContent { get; set; }
// TODO EpicCreatedAt
    
    
    /// <summary>
    ///     The id of the returned Bug
    /// </summary>
    public Guid? BugId { get; set; }
    /// <summary>
    /// Title of the returned Bug
    /// </summary>
    public string? BugTitle { get; set; }
    /// <summary>
    /// Title of the returned Bug
    /// </summary>
    public string? BugContent { get; set; }
    
    /// <summary>
    /// DateTime of the creation of the Bug
    /// </summary>
    public DateTime? BugCreatedAt { get; set; }
}