namespace Core.Contracts.Dtos
{
    public record AdminCreateDto(Guid ProfileId, DateTime CreatedAt, DateTime UpdatedAt);
}