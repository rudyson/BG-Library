namespace BGNet.TestAssignment.Models.Requests.Auth;

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}