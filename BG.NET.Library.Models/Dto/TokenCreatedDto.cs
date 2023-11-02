namespace BG.NET.Library.Models.Dto;

public class TokenCreatedDto
{
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
}