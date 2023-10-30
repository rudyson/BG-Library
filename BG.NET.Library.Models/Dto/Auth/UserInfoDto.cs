namespace BG.NET.Library.Models.Dto.Auth;

public class UserInfoDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public string? Address { get; set; }
}