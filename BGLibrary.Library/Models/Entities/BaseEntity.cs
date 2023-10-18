using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Entities;

public class BaseEntity
{
    [Required]
    public int Id { get; set; }
}