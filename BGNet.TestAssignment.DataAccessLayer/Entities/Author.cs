namespace BGNet.TestAssignment.DataAccess.Entities;

public class Author : BaseEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required DateOnly Birthday { get; set; }

    public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();
}