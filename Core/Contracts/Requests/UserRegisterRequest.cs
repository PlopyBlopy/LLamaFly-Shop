namespace Core.Contracts.Requests
{
    public record UserRegisterRequest(string Login, string Email, string PhoneNumber, string Password);
}