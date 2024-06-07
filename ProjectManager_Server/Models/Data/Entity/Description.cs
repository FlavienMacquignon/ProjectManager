using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  ProjectManager_Server.Models.Data.Entity;

/// <summary>
/// Description Table
/// </summary>
[Table("description", Schema = "data")]
public class Description
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Human Readable title
    /// </summary>
    [Required]
    [Column("title")]
    public string Title { get; set; }

    /// <summary>
    /// Description of bug
    /// </summary>
    [Column("content")]
    public string? Content { get; set; }
}