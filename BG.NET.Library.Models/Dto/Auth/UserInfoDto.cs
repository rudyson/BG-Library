namespace BG.NET.Library.Models.Dto.Auth;

public class UserInfoDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public required string Address { get; set; }
}