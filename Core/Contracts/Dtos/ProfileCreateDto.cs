namespace Core.Contracts.Dtos
{
    public record ProfileCreateDto(Guid Id, string Name, string Surname, string Patronymic, DateTime CreatedAt, DateTime UpdatedAt);
}