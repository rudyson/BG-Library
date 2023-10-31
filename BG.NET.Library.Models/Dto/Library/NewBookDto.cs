using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Library;

public class NewBookDto
{
    public string? Title { get; set; }
    public int PublishYear { get; set; }
    public string? Genre { get; set; }
}