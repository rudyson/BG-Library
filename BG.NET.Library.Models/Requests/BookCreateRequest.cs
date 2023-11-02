namespace BG.NET.Library.Models.Requests
{
    public class BookCreateRequest
    {
        public required string Title { get; set; }
        public required int PublishYear { get; set; }
        public required string Genre { get; set; }
        public int? AuthorId { get; set; }
    }
}
