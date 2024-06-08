using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectManager_Server.Models.UserLand.Entity;

/// <summary>
/// Role Entity
/// </summary>
public class Roles
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [Column("name")]
    [StringLength(50, ErrorMessage = "Role name cannot be longer than 50 char")]
    public string Name { get; set; } = "";
}