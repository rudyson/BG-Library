using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Identity.Models.Dto;

public class LoginDto
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}