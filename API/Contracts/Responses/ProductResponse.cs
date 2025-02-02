namespace API.Contracts.Responses
{
    public record ProductResponse(Guid Id, string Title, string Description, decimal Price, double Rating, DateTime CreateAt, Guid CategoryId, Guid SellerId);
}