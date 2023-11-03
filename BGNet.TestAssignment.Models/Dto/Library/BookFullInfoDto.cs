namespace BGNet.TestAssignment.Models.Dto.Library;

public class BookFullInfoDto
{
    public int Id { get; set; }
    public AuthorShortInfoDto? Author { get; set; }
    public string? Title { get; set; }
    public int PublishYear { get; set; }
    public string? Genre { get; set; }
}