namespace BG.NET.Library.Models.Requests;

public class AuthorUpdateRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly? Birthday { get; set; }
}