namespace BGNet.TestAssignment.Models.Dto.Auth;

public class TokenCreatedDto
{
    public required string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ExpiresAt { get; set; }
}