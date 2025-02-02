namespace API.Contracts.Requests
{
    public record ProductCreateRequest(string Title, string Description, decimal Price, double Rating, Guid CategoryId, Guid SellerId);
}