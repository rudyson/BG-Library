namespace BGNet.TestAssignment.Models.Dto.Library;

public class AuthorShortInfoDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly Birthday { get; set; }
}