using ProjectManager_Server.Attribute;

namespace ProjectManager_Server.Models.Shared.Internal.Filter;

/// <summary>
///     A Generic Filter Object to hold different type of Filter
/// </summary>
public record GenericFilter
{
    /// <summary>
    ///     The name of the Property to filter
    /// </summary>
    public required string PropertyName { get; set; }
    
    /// <summary>
    ///     The query to compare the property against
    /// </summary>
    public required string Query { get; set; }
    
    /// <summary>
    ///     Should the result be negated (All results NOT Equals to the query) 
    /// </summary>
    public required bool Negated { get; set; }
    
    /// <summary>
    ///     Apply a string filter to the property
    /// </summary>
    [NumberOfNullAuthorized([nameof(BooleanFilter), nameof(TimedFilter)],2,2)]
    public StringFilter? StringFilter { get; set; }
    
    /// <summary>
    ///     Apply a Boolean Filter to the property
    /// </summary>
    public BooleanFilter? BooleanFilter { get; set; }
    
    /// <summary>
    ///     Apply a Timed Filter to the property
    /// </summary>
    public TimedFilter? TimedFilter { get; set; }

}
