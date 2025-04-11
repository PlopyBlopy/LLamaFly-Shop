namespace Core.Contracts.Requests
{
    public record ProductCreateWithRatingRequest(string Title, string Description, decimal Price, double Rating, Guid CategoryId, Guid SellerId);
}