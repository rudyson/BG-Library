namespace BGNet.TestAssignment.Models.Requests.Library;

public class AuthorUpdateRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly? Birthday { get; set; }
}