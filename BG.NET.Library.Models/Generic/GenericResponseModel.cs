namespace BG.NET.Library.Models.Generic
{
    public class GenericResponseModel<T> where T : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
        public bool HasValue => Value != null;
        public List<string>? Errors { get; set; }
    }
}