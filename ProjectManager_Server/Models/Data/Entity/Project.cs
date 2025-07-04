using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManager_Server.Models.UserLand.Entity;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
///     Entity that contains all epics and bugs for a given development
/// </summary>
[Table("project", Schema = "data")]
public class Project
{
    /// <summary>
    ///     id of the project
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     id of description for this project
    /// </summary>
    [Column("description_id")]
    public Guid DescriptionId { get; set; }

    /// <summary>
    ///     Description Object
    /// </summary>
    [ForeignKey("DescriptionId")]
    public Description? Description { get; set; }

    /// <summary>
    ///     Foreign Key Reporter
    /// </summary>
    [Column("reported_id")]
    public Guid ReporterId { get; set; }

    /// <summary>
    ///     Reporter (User) Entity
    /// </summary>
    [ForeignKey("ReporterId")]
    public User? Reporter { get; set; }

    /// <summary>
    ///     Foreign Key Assignated
    /// </summary>
    [Column("assignated_id")]
    public Guid? AssignatedId { get; set; }

    /// <summary>
    ///     Assignee (User) Entity
    /// </summary>
    [ForeignKey("AssignatedId")]
    public User? Assignee { get; set; }
    
    /// <summary>
    ///     DateTime of creation of the entry
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    /// <summary>
    ///     List of Epics for this Project
    /// </summary>
    [InverseProperty("Project")]
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Epic> Epics { get; set; } = [];
}