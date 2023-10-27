namespace BG.NET.Library.Models.Dto.Library;

public class AuthorDtoNoBooks : AuthorDtoBase
{
    public int Id { get; set; }
    // Amount of books, Count
    public int Books { get; set; }
}