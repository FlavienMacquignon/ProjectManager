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
    public required string Title { get; set; }

    /// <summary>
    ///     Description of bug
    /// </summary>
    [Column("content")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public string? Content { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
}