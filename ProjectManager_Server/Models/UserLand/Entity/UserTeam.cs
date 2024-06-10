using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
/// Join entity between Users and Team
/// </summary>
public class UserTeam
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// User Id
    /// </summary>
    [Column("user_id")]
    public Guid UserId { get; set;}

    /// <summary>
    /// Team Id
    /// </summary>
    [Column("team_id")]
    public Guid TeamId { get; set;}
}