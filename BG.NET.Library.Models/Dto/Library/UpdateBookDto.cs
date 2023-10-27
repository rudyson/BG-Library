namespace BG.NET.Library.Models.Dto.Library;

public class UpdateBookDto : NewBookDto
{
    public int? AuthorId { get; set; }
}