namespace Core.Contracts.Requests
{
    public record ProductUpdateRequest(Guid Id, string? Title, string? Description, decimal? Price, Guid? CategoryId);
}