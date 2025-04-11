namespace Core.Contracts.Dtos
{
    public record UserUpdateDto(Guid Id, string? Login, string? Email, string? PhoneNumber, DateTime UpdatedAt);
}