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

    /// <summary>
    /// First Name of User
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    [Column("first_name")]
    [StringLength(25, ErrorMessage ="FirstName cannot be more than 25 char")]
    public string FirstName { get; set; }="";

    /// <summary>
    /// Last Name of User
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    [Column("last_name")]
    [StringLength(50, ErrorMessage = "LastName cannot be more than 50 char")]
    public string LastName { get; set;}="";

    /// <summary>
    /// Email of User
    /// </summary>
    [Required(ErrorMessage ="Email is required")]
    [Column("email")]
    [EmailAddress]
    public string Email { get; set;}="";

    /// <summary>
    /// Icone to display for user
    /// </summary>
    [Column("icone")]
    public string icone {get; set;}="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAACXBIWXMAAAsTAAALEwEAmpwYAAACCElEQVR4nO2Zz0obURTGf2OLpi/QuBI3Lly22gdw0624skEUQaHpSlqDfQHFVWteQoX2Abop9RF04cptEkFREFMoYmvKhTMg004wOndyz3h+8EGYG5L7fffm/jkBwzAMwzAMwzAMwzAMw/BNBEwAc8B7kXv9UtoKyzBQB1pAJ0WubQsoUyAiYA342cV4Um2gVoQZUQJ2ezCe1I58hkoiYPsB5mN91ToT1jIwH2sVZTwHLjMMoC2LqBrqGZqP9RklDADHHgJoaVkLJj2Yj+UOS8Ez5zGACgqoeQxAxW6w6jGADyig4jGAWRQw4TGAFyggAhoezDe1bIPIlTbrAD7xyI/CZZRRyzAAVzV6tNfhL5p++0lKUtS4r/ltzQWRmEgOR+0ejF/KtFc78v+jLFfaZhfjDVnt3SJaWCI50FRulcUr8qxQI24Yxj+U5LL0BqgCH0VVeebahigQEfAK2AAOgOs7bIHuPfvAupTXVC6Mg8ACcJjBSfAIWAGeoYBIaoLd/gC9r5qh1wRHgT0PxpP6AYwQGFPASQ7mY50BrwmEt8DvHM3Hct+53G/zS8CfPpiPdQO865f5GelAp89yAzCdt/kx4CIA87evz+N5mX8iB5VOYHJ9eppHAPMBmE3TYh4BfAvAaJq+5xHAaQBG03SeRwC/AjCaJtc372wCVwGYTepKbp2GYXBn/gIcmHnJ2f8WNwAAAABJRU5ErkJggg==";

    /// <summary>
    /// Id of the Team of the User
    /// </summary>
    [Column("team_id")]
    public Guid TeamId { get; set; }
}