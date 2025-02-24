namespace Core.Contracts.Requests
{
    public record UserLoginRequest(string Identifier, string password);
}