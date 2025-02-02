namespace Core.Contracts.Dto
{
    public record ProductCreateDto(string Title, string Description, decimal Price, double Rating, Guid CategoryId, Guid SellerId);
}