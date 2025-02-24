namespace Core.Contracts.Dtos
{
    public record ProductFiltersDto(string? Search, Guid? CategoryId, string? SortProp, string? SortOrder);
}
