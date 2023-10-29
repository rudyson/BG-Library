namespace BG.NET.Library.Models.Dto.Library;

public class BookDtoUpdate
{
    public string? Title { get; set; }
    public int? PublishYear { get; set; }
    public string? Genre { get; set; }
    public int? AuthorId { get; set; }
}