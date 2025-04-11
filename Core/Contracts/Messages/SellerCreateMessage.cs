namespace Core.Contracts.Messages
{
    public record SellerCreateMessage(Guid ProfileId, DateTime CreatedAt, DateTime UpdatedAt);
}