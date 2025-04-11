namespace Core.Contracts.Dtos
{
    public record ProductSellerDto(Guid Id, string Title, string Description, decimal Price, Guid CategoryId, DateTime UpdatedAt, DateTime CreatedAt);
}