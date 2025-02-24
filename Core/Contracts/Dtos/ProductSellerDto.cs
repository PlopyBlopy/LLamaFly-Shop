namespace Core.Contracts.Dtos
{
    public record ProductSellerDto(Guid Id, string Title, string Description, decimal Price, DateTime CreatedAt, Guid CategoryId);
}
