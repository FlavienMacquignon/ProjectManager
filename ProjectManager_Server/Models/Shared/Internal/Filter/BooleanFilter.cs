namespace ProjectManager_Server.Models.Shared.Internal.Filter;

/// <summary>
///     Boolean Filter
/// </summary>
public record BooleanFilter
{
    /// <summary>
    ///     Should the tested property be True?
    /// </summary>
    public bool ShouldBeTrue { get; set; }
}