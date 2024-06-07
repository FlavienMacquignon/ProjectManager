using System.ComponentModel.DataAnnotations;

namespace ProjectManager_Server.Models.ViewModels;

// TODO Document

/// <summary>
/// 
/// </summary>
/// <value></value>
public record BugContentViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 255 char")]
    public string Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string Content { get; set; }

}