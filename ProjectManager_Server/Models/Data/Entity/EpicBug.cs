using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
///     Linking Entity (Bug to Epic)
/// </summary>
public class EpicBug
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     id of Epic
    /// </summary>
    [Column("epic_id")]
    public Guid EpicId { get; set; }

    /// <summary>
    ///     id of Bug
    /// </summary>
    [Column("bug_id")]
    public Guid BugId { get; set; }
}