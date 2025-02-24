namespace Core.Contracts.Dtos
{
    public record ProductCreateDto(string Title, string Description, decimal Price, Guid CategoryId, Guid SellerId);
}
