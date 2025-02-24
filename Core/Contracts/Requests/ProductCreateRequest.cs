namespace Core.Contracts.Requests
{
    public record ProductCreateRequest(string Title, string Description, decimal Price, Guid CategoryId, Guid SellerId);
    //public record ProductCreateRequest(string Title, string Description, decimal Price, double Rating, Guid CategoryId, Guid SellerId);
}
