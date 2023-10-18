using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Entities;

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