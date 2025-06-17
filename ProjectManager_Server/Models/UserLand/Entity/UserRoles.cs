using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
///     Join entity between Users and Roles
/// </summary>
public class UserRoles
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    [Column("user_id")]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Role Id
    /// </summary>
    [Column("role_id")]
    public Guid RoleId { get; set; }
}