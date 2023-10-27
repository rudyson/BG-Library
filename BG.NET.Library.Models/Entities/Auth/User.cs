using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Entities.Auth;

public class User : BaseEntity
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }
    [Required]
    public string? Address { get; set; }
}