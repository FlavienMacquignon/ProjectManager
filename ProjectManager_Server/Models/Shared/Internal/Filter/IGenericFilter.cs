namespace ProjectManager_Server.Models.Shared.Internal.Filter;

public record IGenericFilter
{
    public string propertyName { get; set; }
    public string query { get; set; }
    public bool? negated { get; set; }
    public StringBasedFilter? stringFilter { get; set; }
    public BooleanFilter? BooleanFilter { get; set; }
    public TimedFilter? TimedFilter { get; set; }

}
