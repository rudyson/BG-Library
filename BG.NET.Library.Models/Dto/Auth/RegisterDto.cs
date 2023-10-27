using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Auth;

public class RegisterDto
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