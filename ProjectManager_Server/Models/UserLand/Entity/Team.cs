using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
/// Team Entity
/// </summary>
public class Team
{
    /// <summary>
    /// Id of Entity
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
}