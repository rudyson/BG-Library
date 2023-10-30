namespace BG.NET.Library.Models.Dto.Auth;

public class JwtTokenDto
{
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
}