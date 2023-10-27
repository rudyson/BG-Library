using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.Models.Entities;

public class BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
}