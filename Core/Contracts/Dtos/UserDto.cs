namespace Core.Contracts.Dtos
{
    public record UserDto(Guid Id, string Role, string Login, string Password);
}
