namespace ProjectManager_Server.Models.Shared.Internal.Filter;

/// <summary>
///     String Based Filter
/// </summary>
public record StringFilter
{ 
    /// <summary>
    ///     True if the result should exactly match the provided query
    /// </summary>
    public bool? StrictlyEqual { get; set; }
}