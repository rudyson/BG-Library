namespace BG.NET.Library.Models.Requests;

public class RegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required DateOnly Birthday { get; set; }
    public required string Address { get; set; }
}