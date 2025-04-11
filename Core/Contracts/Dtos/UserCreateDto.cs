namespace Core.Contracts.Dtos
{
    public record UserCreateDto(Guid Id, string Role, string Login, string Email, string PhoneNumber, DateTime CreatedAt, DateTime UpdatedAt);
}