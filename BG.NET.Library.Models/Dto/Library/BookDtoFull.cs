namespace BG.NET.Library.Models.Dto.Library;

public class BookDtoFull : BookDtoBase
{
    public int Id { get; set; }
    public AuthorDtoNoBooks? Author { get; set; }
}