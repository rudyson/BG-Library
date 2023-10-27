using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Auth;

public class LoginDto
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}