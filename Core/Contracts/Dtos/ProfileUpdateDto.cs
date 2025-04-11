namespace Core.Contracts.Dtos
{
    public record ProfileUpdateDto(Guid Id, string? Name, string? Surname, string? Patronymic, DateTime UpdatedAt);
}