namespace Core.Contracts.Messages
{
    public record CustomerCreateMessage(Guid ProfileId, DateTime CreatedAt, DateTime UpdatedAt);
}