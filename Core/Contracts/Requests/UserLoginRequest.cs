namespace Core.Contracts.Requests
{
    public record UserLoginRequest(string? Login, string? Email, string? PhoneNumber, string password);
}