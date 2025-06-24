using System;

namespace ProjectManager_Server.Models.Data.ViewModels.UpBug;

/// <summary>
///     A ViewModel to transfer data to update a Bug
/// </summary>
/// <param name="Id">The id of the entity to update</param>
/// <param name="Description">Description to update</param>
/// <param name="EpicId">The attached Epic to this Bug</param>
/// <param name="AssignatedId">The User assigned to this Bug</param>
/// <param name="ClosedAt">The timestamp at witch this bug was closed</param>
public record UpBugViewModel (Guid Id, UpDescriptionContentViewModel? Description, Guid? EpicId, Guid? AssignatedId, string? ClosedAt);