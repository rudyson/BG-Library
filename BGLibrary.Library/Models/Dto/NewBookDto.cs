using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Dto;

public class NewBookDto
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public int PublishYear { get; set; }
    [Required]
    public string? Genre { get; set; }
}