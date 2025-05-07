namespace ProjectManager_Server.Models.Shared.Internal.Filter;

public record TimedFilter
{
    public bool? before { get; set; }
    public bool? after { get; set; }
    public bool? include { get; set; }
}
