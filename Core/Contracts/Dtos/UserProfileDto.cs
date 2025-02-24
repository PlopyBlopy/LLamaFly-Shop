namespace Core.Contracts.Dtos
{
    public record UserProfileDto(Guid id, string Role, string Login, string Email, string? PhoneNumber);
}
