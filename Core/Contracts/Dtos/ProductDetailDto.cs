namespace Core.Contracts.Dtos
{
    public record ProductDetailDto(Guid Id, string Title, string Description, decimal Price, double Rating);
}