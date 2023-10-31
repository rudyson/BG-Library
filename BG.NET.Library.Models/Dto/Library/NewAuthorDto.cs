using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Dto.Library;

public class NewAuthorDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly Birthday { get; set; }
}