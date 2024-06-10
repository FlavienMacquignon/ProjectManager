using System;
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
    public string? Content { get; set; }

    /// <summary>
    /// Id of the project linked to this bug
    /// </summary>
    public Guid ProjectId {get;set;}

    /// <summary>
    /// Id of the user that reported this bug
    /// </summary>
    public Guid ReporterId { get; set; }
}