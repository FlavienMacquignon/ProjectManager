using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
/// Linking Entity (Bug to Project)
/// </summary>
public class ProjectBug
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set;}

    /// <summary>
    /// Id of Epic
    /// </summary>
    [Column("project_id")]
    public Guid ProjectId { get; set;}

    /// <summary>
    /// Id of Bug
    /// </summary>
    [Column("bug_id")]
    public Guid BugId { get; set;}
}
