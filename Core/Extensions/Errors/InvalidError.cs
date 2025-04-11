using FluentResults;

namespace Core.Extensions.Errors
{
    public class InvalidError : IError
    {
        public List<IError>? Reasons => null;
        public string Message { get; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public InvalidError(string errorCode, string entityName)
        {
            Message = $"{entityName} invalid.";
            Metadata.Add("errorCode", $"{entityName}.Invalid");
            Metadata.Add("entity", entityName);
        }

        public InvalidError(string errorCode, string entityName, Guid key)
        {
            Message = $"{entityName} invalid.";
            Metadata.Add("errorCode", $"{entityName}.Invalid");
            Metadata.Add("entity", entityName);
            Metadata.Add("key", key);
        }
    }
}