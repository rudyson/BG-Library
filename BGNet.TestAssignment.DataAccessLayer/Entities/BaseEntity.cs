using System.ComponentModel.DataAnnotations;

namespace BGNet.TestAssignment.DataAccess.Entities;

public class BaseEntity
{
    [Key]
    public required int Id { get; set; }
}