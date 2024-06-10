using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
/// Linking Entity (Project to Epic)
/// </summary>
public class EpicProject
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
