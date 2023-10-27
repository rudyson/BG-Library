using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Entities.Library;

public class Author : BaseEntity
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }

    public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();
}