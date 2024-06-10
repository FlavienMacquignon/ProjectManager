using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManager_Server.Models.UserLand.Entity;
using ProjectManager_Server.Models.ViewModels;

namespace ProjectManager_Server.Models.Data.Entity;


/// <summary>
/// Bug Entity
/// </summary>
[Table("bug", Schema = "data")]
public class Bug
{

    /// <summary>
    /// Default ctor used by Entity Framework
    /// </summary> 
    public Bug() { }

    /// <summary>
    /// Build a Bug from a BugContent View Model
    /// </summary>
    /// <param name="bc">A Bug Content View Model</param>
    public Bug(DescriptionContentViewModel bc)
    {
        Description = new Description
        {
            Title = bc.Title,
            Content = bc.Content
        };
        ProjectId = bc.ProjectId;
        ReporterId = bc.ReporterId;
    }

    /// <summary>
    /// Id of entity
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Human friendly id
    /// </summary>
    // TODO Pretty confident that Entity can do it for me
    [Column("display_id")]
    public int DisplayId { get; set; }

    /// <summary>
    ///     Id of the attached description item for this bug
    /// </summary>
    [Column("description_id")]
    public Guid DescriptionId { get; set; }

    /// <summary>
    /// Description Object
    /// </summary>
    [ForeignKey("DescriptionId")]
    public Description Description { get; set; }

    /// <summary>
    /// Date time at witch the bug has been created
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    /// <summary>
    /// Foreign Key project
    /// </summary>
    [Required]
    [Column("project_id")]
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Project entity
    /// </summary>
    [Required]
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }

    /// <summary>
    /// Foreign Key Epic 
    /// </summary>
    [Column("epic_id")]
    public Guid? EpicId { get; set; }

    /// <summary>
    /// Epic Entity
    /// </summary>
    [ForeignKey("EpicId")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public Epic? Epic { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    /// <summary>
    /// Foreign Key Reporter
    /// </summary>
    [Column("reporter_id")]
    public Guid ReporterId { get; set; }

    /// <summary>
    /// Reporter (User) Entity
    /// </summary>
    [ForeignKey("ReporterId")]
    public User Reporter {get;set;}

    /// <summary>
    /// Foreign Key Assignated
    /// </summary>
    [Column("assignated_id")]
    public Guid? AssignatedId { get; set; }

    /// <summary>
    /// Assignee (User) Entity
    /// </summary>
    [ForeignKey("AssignatedId")]
    public User Assignee {get;set;}

    /// <summary>
    /// DateTime where Bug is closed
    /// </summary>
    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    /// <summary>
    /// Utility prop, is the bug closed ?
    /// </summary>
    [NotMapped]
    public bool IsCompleted => ClosedAt != null;

    /// <summary>
    ///     Create a <see cref="DescriptionContentViewModel"/> from this entity
    /// </summary>
    /// <returns>This bug as a DescriptionViewModel</returns>
    public DescriptionContentViewModel ToDescriptionContentViewModel(){
    
        return new DescriptionContentViewModel(){
            Title=Description.Title, 
            Content=Description.Content, 
            ProjectId = ProjectId, 
            ReporterId = ReporterId};
    }
}
