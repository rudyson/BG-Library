namespace BG.NET.Library.Models.Dto.Library;

public class AuthorDtoNoBooks : AuthorDtoBase
{
    public int Id { get; set; }
    public int Books { get; set; }
}