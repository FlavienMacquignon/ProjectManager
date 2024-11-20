using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
///     Description Table
/// </summary>
[Table("description", Schema = "data")]
public class Description
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Human Readable title
    /// </summary>
    [Required]
    [Column("title")]
    [StringLength(50)]
    public required string Title { get; set; }

    /// <summary>
    ///     Description of bug
    /// </summary>
    [Column("content")]
    [StringLength(500)]
    public string? Content { get; set; }
}