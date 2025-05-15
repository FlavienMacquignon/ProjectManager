namespace ProjectManager_Server.Models.Data.ViewModels;

/// <summary>
///     Response returned for a search
/// </summary>
public record Responses
{
    /// <summary>
    ///     A epic that match filters for this search
    /// </summary>
    public EpicViewModel? Epics { get; set; }
    
    /// <summary>
    ///     A Bug that match filters for this search
    /// </summary>
    public BugViewModel? Bugs { get; set; }
}
