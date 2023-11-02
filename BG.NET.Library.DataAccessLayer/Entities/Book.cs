namespace BG.NET.Library.DataAccess.Entities;

public class Book : BaseEntity
{
    public required string Title { get; set; }
    public required int PublishYear { get; set; }
    public required string Genre { get; set; }

    public Author? Author { get; set; }
}