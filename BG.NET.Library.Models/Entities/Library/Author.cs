using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Entities.Library;

public class Author : BaseEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required DateOnly Birthday { get; set; }

    public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();
}