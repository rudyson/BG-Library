namespace BGNet.TestAssignment.Models.Dto.Library;

public class AuthorFullInfoDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public IEnumerable<BookShortInfoDto>? Books { get; set; }
}