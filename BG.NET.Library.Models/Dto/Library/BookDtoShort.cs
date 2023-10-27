namespace BG.NET.Library.Models.Dto.Library;

public class BookDtoShort : BookDtoBase
{
    public int Id { get; set; }
    public int? AuthorId { get; set; }
}