using System;
using System.Collections.Generic;

namespace ProjectManager_Server.Models.Data.ViewModels;

public record EpicViewModel
{
    public string Title { get; internal set; }
    public string? Content { get; internal set; }
    public Dictionary<Guid, string> BugsMinimalDescription { get; set; }
}