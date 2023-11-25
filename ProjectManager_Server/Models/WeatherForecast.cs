using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager_Server.Models;

public class Bug
{
    [Key]
    public Guid Id {get;set;}

    public int DisplayId {get;set;}

    [StringLength(255, MinimumLength =1, ErrorMessage ="Title must be between 1 and 255 char")]
    public string Title {get;set;}

    public string Content {get;set;}

    public DateTime Date { get; set; }
}
