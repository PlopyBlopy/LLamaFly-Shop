namespace Core.Contracts.Requests
{
    public record UserRegisterRequest(string Role, string Login, string Email, string? PhoneNumber, string Password);
}