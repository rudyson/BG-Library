namespace BG.NET.Library.Models.Dto.Library;

public class AuthorDtoFull : AuthorDtoNoBooks
{
    public IEnumerable<BookDtoBase>? Books { get; set; }
}