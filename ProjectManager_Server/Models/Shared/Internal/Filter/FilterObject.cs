using System;
using System.Collections.Generic;
using ProjectManager_Server.Managers;

namespace ProjectManager_Server.Models.Shared.Internal.Filter;

public record FilterObject
{
    public Guid projectId { get; set; }
    public List<IGenericFilter>? epicFilters { get; set; }
    public List<IGenericFilter>? bugFilters { get; set; }
}
