namespace Core.Contracts.Dtos
{
    public record ProductUpdateDto(Guid Id, string? Title, string? Description, decimal? Price, Guid? CategoryId);
}
