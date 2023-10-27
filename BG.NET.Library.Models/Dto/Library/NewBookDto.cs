using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Library;

public class NewBookDto
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public int PublishYear { get; set; }
    [Required]
    public string? Genre { get; set; }
}