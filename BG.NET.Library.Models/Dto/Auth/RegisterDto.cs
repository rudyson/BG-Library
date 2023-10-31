using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Auth;

public class RegisterDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required DateOnly Birthday { get; set; }
    public required string Address { get; set; }
}