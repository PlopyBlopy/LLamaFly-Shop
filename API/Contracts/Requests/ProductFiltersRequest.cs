namespace API.Contracts.Requests
{
    public record ProductFiltersRequest(string? Search, Guid? CategoryId, string? SortProp, string? SortOrder);
}