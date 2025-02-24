namespace Core.Contracts.Dtos
{
    public record UserRegisterDto(string Role, string Login, string Password, string Email, string? PhoneNumber);
}
