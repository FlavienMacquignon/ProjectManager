using System.ComponentModel.DataAnnotations;

namespace ProjectManager_Server.Models.ViewModels;


/// <summary>
///     Content of description
/// </summary>
public record DescriptionContentViewModel
{
    /// <summary>
    /// Title
    /// </summary>
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 255 char")]
    public string Title { get; set; }

    /// <summary>
    /// Content / Description
    /// </summary>
    public string Content { get; set; }

}