namespace BG.NET.Library.Models.Dto.Library;

public class AuthorDtoFull : AuthorDtoNoBooks
{
    public new IEnumerable<BookDtoBase>? Books { get; set; }
}