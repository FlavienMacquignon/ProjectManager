using System;

namespace ProjectManager_Server.Models.Shared.Internal;

public record SearchResult //TODO DOC 
{
    public Guid? EpicId { get; set; }
    public string EpicTitle { get; set; }
    public string EpicContent { get; set; }
    public Guid? BugId { get; set; }
    public string BugTitle { get; set; }
    public string BugContent { get; set; }
}