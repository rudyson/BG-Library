using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Identity.Models.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }
    [Required]
    public string? Address { get; set; }
}