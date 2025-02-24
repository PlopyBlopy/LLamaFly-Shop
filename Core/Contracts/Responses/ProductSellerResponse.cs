namespace Core.Contracts.Responses
{
    public record ProductSellerResponse(Guid Id, string Title, string Description, decimal Price, DateTime CreatedAt, Guid CategoryId);
}