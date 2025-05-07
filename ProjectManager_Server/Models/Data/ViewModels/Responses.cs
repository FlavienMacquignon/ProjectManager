namespace ProjectManager_Server.Models.Data.ViewModels;

/// <summary>
/// 
/// </summary>
public record Responses
{
    /// <summary>
    /// 
    /// </summary>
    public EpicViewModel? Epics { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DescriptionContentViewModel? Bugs { get; set; }
}
