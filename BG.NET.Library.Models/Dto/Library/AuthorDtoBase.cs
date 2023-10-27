namespace BG.NET.Library.Models.Dto.Library;

public class AuthorDtoBase
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly Birthday { get; set; }
}