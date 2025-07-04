using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ProjectManager_Server.Models.Data.ViewModels.GetOneEpic;
using ProjectManager_Server.Models.UserLand.Entity;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
///     Epic group bugs to represent a functionality
/// </summary>
[Table("epic", Schema = "data")]
public class Epic
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Description of the Epic
    /// </summary>
    [Column("description_id")]
    public Guid DescriptionId { get; set; }

    /// <summary>
    ///     Description Entity
    /// </summary>
    [ForeignKey("DescriptionId")]
    public Description? Description { get; set; }

    /// <summary>
    ///     Project to link the Epic to
    /// </summary>
    [Column("project_id")]
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Project Entity
    /// </summary>
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }

    /// <summary>
    ///     Foreign Key Reporter
    /// </summary>
    [Column("reporter_id")]
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
    ///     List of Bugs in the Epic
    /// </summary>
    [InverseProperty("Epic")]
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Bug> Bugs { get; set; } = new();

    /// <summary>
    ///     Is the Epic completed ?? (AKA is there any bug incomplete???)
    /// </summary>
    [NotMapped]
    public bool IsComplete { get; set; }

    /// <summary>
    ///     Convert this entity in ViewModel
    /// </summary>
    /// <returns>A representation of this entity</returns>
    public OneEpicViewModel ToViewModel()
    {
        ArgumentNullException.ThrowIfNull(Description);
        return new OneEpicViewModel
        {
            Title = Description.Title,
            Content = Description.Content,
            BugsMinimalDescription = Bugs.AsEnumerable().ToDictionary(bug => bug.Id, bug => bug.Description!.Title)
        };
    }
}