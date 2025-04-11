namespace Core.Contracts.Dtos
{
    public record CustomerCreateDto(Guid ProfileId, DateTime CreatedAt, DateTime UpdatedAt);
}