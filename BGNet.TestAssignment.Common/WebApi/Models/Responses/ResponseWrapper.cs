namespace BGNet.TestAssignment.Common.WebApi.Models.Responses
{
    public class ResponseWrapper<T> where T : class
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public ResponseWrapper(T? data)
        {
            Data = data;
        }

        public ResponseWrapper(IEnumerable<string>? errors)
        {
            Errors = errors;
        }

        // Status200OK
        public static ResponseWrapper<T> Wrap(T? data = null)
        {
            if (data == null)
            {
                return new ResponseWrapper<T>(new List<string> { "Data is null" }) { Status = 204 };
            }
            return new ResponseWrapper<T>(data) { Status = 200 };
        }
        // Status400BadRequest
        public static ResponseWrapper<T> Wrap(IEnumerable<string>? errors = null)
        {
            if (errors == null)
            {
                return new ResponseWrapper<T>(new List<string> { "Internal server error" }) { Status = 500 };
            }
            return new ResponseWrapper<T>(errors) { Status = 500 };
        }

        public static ResponseWrapper<T> Wrap(ResponseCodes code)
        {
            string message = (code) switch
            {
                ResponseCodes.NotFound => "Object not found",
                ResponseCodes.DeleteRequestFailed => "Unable to delete object",
                ResponseCodes.CreateRequestFailed => "Unable to create object",
                ResponseCodes.NothingToUpdate => "All fields of object are up to date",
                ResponseCodes.PaginationBroken => "Pagination broken",
                ResponseCodes.EmptyQuery => "Empty query",
                ResponseCodes.WrongAuthorizationToken => "Wrong authorization token",
                _ => "Internal server error",
            };
            return new ResponseWrapper<T>(new List<string> { message }) { Status = 500 };
        }

        // Status422UnprocessableEntity
        public static ResponseWrapper<T> Wrap(IDictionary<string, string[]>? validation = null)
        {
            if (validation == null)
            {
                return new ResponseWrapper<T>(new List<string> { "Empty validation error" }) { Status = 500 };
            }
            else
            {
                var validationToList = validation.Select(x => String.Join(": ", x.Key, String.Join("; ", x.Value)));
                return new ResponseWrapper<T>(validationToList) { Status = 422 };
            }
        }
        public static ResponseWrapper<T> Wrap(string errorMessage)
        {
            return new ResponseWrapper<T>(new List<string> { errorMessage }) { Status = 500 };
        }
        public int Status { get; set; }

        public bool HasData => Data != null;
        public bool HasErrors => Errors != null;
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
