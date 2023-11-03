namespace BGNet.TestAssignment.Models.Requests.Library
{
    public class BookUpdateRequest
    {
        public string? Title { get; set; }
        public int? PublishYear { get; set; }
        public string? Genre { get; set; }
        public int? AuthorId { get; set; }
    }
}
