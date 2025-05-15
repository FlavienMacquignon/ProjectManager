namespace ProjectManager_Server.Models.Shared.Internal.Filter;

/// <summary>
///     Timed Filter 
/// </summary>
public record TimedFilter
{
    /// <summary>
    ///     Does the results should be before the provided value in the <see cref ="GenericFilter.Query"/>
    /// </summary>
    public bool? Before { get; set; }
    
    /// <summary>
    ///     Does the results should be after the provided value in the <see cref ="GenericFilter.Query"/>
    /// </summary>
    public bool? After { get; set; }
    
    /// <summary>
    ///     Does the result include with the same value as provided in <see cref="GenericFilter.Query"/> be included in result? 
    /// </summary>
    public bool? Include { get; set; }
}
