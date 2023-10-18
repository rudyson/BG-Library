using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Dto;

public class NewAuthorDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }
}