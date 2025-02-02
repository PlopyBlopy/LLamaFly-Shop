namespace Core.Contracts.Dto
{
    public record ProductDto(Guid Id, string Title, string Description, decimal Price, double Rating, DateTime CreateAt, Guid CategoryId, Guid SellerId);
}