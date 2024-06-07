using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
/// User Entity
/// </summary>
[Table("user", Schema ="user_land")]
public class User{

    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
}