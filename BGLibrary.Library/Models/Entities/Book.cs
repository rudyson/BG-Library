using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Entities;

public class Book : BaseEntity
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public int PublishYear { get; set; }
    [Required]
    public string? Genre { get; set; }
    
    public Author? Author { get; set; }
}