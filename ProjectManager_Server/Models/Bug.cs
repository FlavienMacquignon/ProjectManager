using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager_Server.Models;

/// <summary>
/// Bug Entity
/// </summary>
public class Bug
{
    /// <summary>
    /// Id of entity
    /// </summary>
    // TODO Make it DB generated
    [Key]
    public Guid Id {get;set;}

/// <summary>
/// Human friendly id
/// </summary>
    public int DisplayId {get;set;}

    /// <summary>
    /// Title of the Bug, for now limitation are totaly random but demonstrate constraint
    /// </summary>
    [StringLength(255, MinimumLength =1, ErrorMessage ="Title must be between 1 and 255 char")]
    public string Title {get;set;}

    /// <summary>
    /// The body of the Bug where a longer description can take place
    /// </summary>
    /// <value></value>
    public string Content {get;set;}

    /// <summary>
    /// Date time at witch the bug has been created
    /// </summary>
    /// <value></value>
    public DateTime Date { get; set; }
}
