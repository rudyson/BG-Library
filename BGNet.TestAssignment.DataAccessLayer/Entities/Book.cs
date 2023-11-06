namespace BGNet.TestAssignment.DataAccess.Entities;

public class Book : BaseEntity
{
    public required string Title { get; set; }
    public required int PublishYear { get; set; }
    public required string Genre { get; set; }

    public required int AuthorId { get; set; }
    public required Author Author { get; set; }
}