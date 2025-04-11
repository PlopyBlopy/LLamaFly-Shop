using FluentResults;

namespace Core.Extensions.Errors
{
    public class NotFoundError : IError
    {
        public List<IError>? Reasons => null;
        public string Message { get; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public NotFoundError(string errorCode, string entityName)
        {
            Message = $"{entityName} not found error.";
            Metadata.Add("errorCode", $"{entityName}.NotFound");
            Metadata.Add("entity", entityName);
        }

        public NotFoundError(string errorCode, string entityName, Guid key)
        {
            Message = $"{entityName} not found error.";
            Metadata.Add("errorCode", $"{entityName}.NotFound");
            Metadata.Add("entity", entityName);
            Metadata.Add("key", key);
        }
    }
}