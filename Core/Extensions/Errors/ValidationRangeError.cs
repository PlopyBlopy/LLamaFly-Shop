using FluentResults;

namespace Core.Extensions.Errors
{
    public sealed class ValidationRangeError : IError
    {
        public List<IError> Reasons => null;
        public List<List<IError>> ReasonsRange { get; set; } = new List<List<IError>>();

        public string Message { get; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public ValidationRangeError(string errorCode, List<List<IError>> fieldErrors)
        {
            Message = "Validation error.";
            Metadata.Add("errorCode", errorCode);
            ReasonsRange.AddRange(fieldErrors);
        }
    }
}