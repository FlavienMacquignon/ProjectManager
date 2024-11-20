using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
///     Team Entity
/// </summary>
public class Team
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Team Name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [Column("name")]
    [StringLength(50, ErrorMessage = "Team Name cannot be longer than 50 char")]
    public string Name { get; set; } = "";

    /// <summary>
    ///     Default role for this team
    /// </summary>
    [Column("default_role")]
    public Guid DefaultRole { get; set; }
}