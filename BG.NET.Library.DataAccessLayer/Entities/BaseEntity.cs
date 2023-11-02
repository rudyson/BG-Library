using System.ComponentModel.DataAnnotations;

namespace BG.NET.Library.DataAccess.Entities;

public class BaseEntity
{
    [Key]
    public required int Id { get; set; }
}