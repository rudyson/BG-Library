using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Library;

public class NewAuthorDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }
}