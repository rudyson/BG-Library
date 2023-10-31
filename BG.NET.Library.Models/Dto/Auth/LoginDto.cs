using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Auth;

public class LoginDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}