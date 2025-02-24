namespace Core.Contracts.Responses
{
    public record ProductResponse(Guid Id, string Title, string Description, decimal Price, double Rating);
}