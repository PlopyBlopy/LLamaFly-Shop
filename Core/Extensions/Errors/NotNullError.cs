using FluentResults;

namespace Core.Extensions.Errors
{
    public class NotNullError : IError
    {
        public List<IError>? Reasons => null;

        public string Message { get; }

        public Dictionary<string, object> Metadata { get; } = new Dictionary<string, object>();

        public NotNullError(string errorCode, string entityName)
        {
            Message = $"Must not be null error.";
            Metadata.Add("errorCode", $"{errorCode}.NotNull");
            Metadata.Add("entity", entityName);
        }

        public NotNullError(string errorCode, string entityName, Guid key)
        {
            Message = $"Must not be null error.";
            Metadata.Add("errorCode", $"{errorCode}.NotNull");
            Metadata.Add("entity", entityName);
            Metadata.Add("key", key);
        }
    }
}