namespace Core.Contracts.Dtos
{
    public record ProductDto(Guid Id, string Title, string Description, decimal Price, double Rating);
}
