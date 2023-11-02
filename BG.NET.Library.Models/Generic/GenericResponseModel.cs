namespace BG.NET.Library.Models.Generic
{
    public class GenericResponseModel<T> where T : class
    {
        public GenericResponseModel() {

        }

        // Status200OK
        public GenericResponseModel(T? value, string? message)
        {
            Success = true;
            Message = message ?? string.Empty;
            Value = value;
        }

        // Status400BadRequest
        public GenericResponseModel(string? message, List<string>? errors)
        {
            Success = false;
            Message = message ?? string.Empty;
            Errors = errors;
        }


        // Status422UnprocessableEntity
        public GenericResponseModel(Dictionary<string, string[]> validation)
        {
            Success = false;
            Message = "Provided request not valid";
            Validation = validation;
            Value = null;
        }
        public bool Success { get; set; }
        public string Message { get; set; } = String.Empty;
        public T? Value { get; set; }

        public bool HasValue => Value != null;
        public bool HasErrors => Errors != null;
        public bool NotValid => Validation != null;
        
        public List<string>? Errors { get; set; }
        public Dictionary<string, string[]>? Validation { get; set; }
    }
}