namespace Core.Contracts.Requests
{
    public record UserUpdateRequest(string? Login, string? Email, string? PhoneNumber);
}