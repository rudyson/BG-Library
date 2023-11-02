namespace BG.NET.Library.Models.Requests
{
    public class BookUpdateRequest
    {
        public string? Title { get; set; }
        public int? PublishYear { get; set; }
        public string? Genre { get; set; }
        public int? AuthorId { get; set; }
    }
}
