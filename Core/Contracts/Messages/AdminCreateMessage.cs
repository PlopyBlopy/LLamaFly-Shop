namespace Core.Contracts.Messages
{
    public record AdminCreateMessage(Guid ProfileId, DateTime CreatedAt, DateTime UpdatedAt);
}