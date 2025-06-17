namespace ProjectManager_Server.Models.Data.ViewModels.Search;

/// <summary>
///     Response returned for a search
/// </summary>
public record Responses
{
    /// <summary>
    ///     A epic that match filters for this search
    /// </summary>
    public SearchEpicViewModel? Epics { get; set; }
    
    /// <summary>
    ///     A Bug that match filters for this search
    /// </summary>
    public BugViewModel? Bugs { get; set; }
}
