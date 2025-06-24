using ProjectManager_Server.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Data.ViewModels.UpBug;
using ProjectManager_Server.Models.UserLand.Entity;

namespace ProjectManager_Server.Models.Data.Entity;

/// <summary>
///     Bug Entity
/// </summary>
[Table("bug", Schema = "data")]
public class Bug
{
    /// <summary>
    ///     Default ctor used by Entity Framework
    /// </summary>
    public Bug()
    {
    }

    /// <summary>
    ///     Build a Bug from a BugContent View Model
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
    ///     id of entity
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
    ///     id of the attached description item for this bug
    /// </summary>
    [Column("description_id")]
    public Guid DescriptionId { get; set; }

    /// <summary>
    ///     Description Object
    /// </summary>
    [ForeignKey("DescriptionId")]
    public Description? Description { get; set; }

    /// <summary>
    ///     Date time at witch the bug has been created
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

    /// <summary>
    ///     Foreign Key project
    /// </summary>
    [Required]
    [Column("project_id")]
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Project entity
    /// </summary>
    [Required]
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }

    /// <summary>
    ///     Foreign Key Epic
    /// </summary>
    [Column("epic_id")]
    public Guid? EpicId { get; set; }

    /// <summary>
    ///     Epic Entity
    /// </summary>
    [ForeignKey("EpicId")]
    public Epic? Epic { get; set; }

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
    ///     DateTime where Bug is closed
    /// </summary>
    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    /// <summary>
    ///     Utility prop, is the bug closed ?
    /// </summary>
    [NotMapped]
    public bool IsCompleted => ClosedAt != null;

    /// <summary>
    ///     Create a <see cref="DescriptionContentViewModel" /> from this entity
    /// </summary>
    /// <returns>This bug as a DescriptionViewModel</returns>
    public DescriptionContentViewModel ToDescriptionContentViewModel()
    {
        ArgumentNullException.ThrowIfNull(Description);
        return new DescriptionContentViewModel
        {
            Title = Description.Title,
            Content = Description.Content,
            ProjectId = ProjectId,
            ReporterId = ReporterId
        };
    }

    /// <summary>
    ///     Update the current entity
    /// </summary>
    /// <param name="updatedData">The object containing the updated datas</param>
    /// <exception cref="InvalidOperationException">If the provided updated datas are not for this object</exception>
    public void Update(UpBugViewModel updatedData)
    {
        if ( Id != updatedData.Id )
            throw new InvalidOperationException("Trying to update the wrong object");
        ArgumentNullException.ThrowIfNull(Description);
        Description.Title = updatedData.Description?.Title ?? Description.Title;
        Description.Content = updatedData.Description?.Content ?? Description.Content;
        EpicId = updatedData.EpicId ?? EpicId;
        AssignatedId = updatedData.AssignatedId ?? AssignatedId;
        //TODO User defined DateFormat system wide
        ClosedAt = updatedData.ClosedAt.ToUniversalDateTimeNullable("yyyy/MM/dd HH:mm:ss") ?? ClosedAt;
    }

    /// <summary>
    ///     Convert the current object into updated data view
    /// </summary>
    /// <returns>A UpBugViewModel</returns>
    public UpBugViewModel ToUpBugVm()
    {
        string? closedAt = null;
        if ( ClosedAt.HasValue )
            //TODO User defined DateFormat system wide
            closedAt = ClosedAt.Value.ToString("yyyy/MM/dd HH:mm:ss");
        ArgumentNullException.ThrowIfNull(Description);
        return new UpBugViewModel(Id,
            new UpDescriptionContentViewModel
                {
                    Title = Description.Title,
                    Content = Description.Content,
                    ProjectId = ProjectId,
                    ReporterId = ReporterId
                },
            EpicId,
            AssignatedId,
            closedAt
            );
    }
}