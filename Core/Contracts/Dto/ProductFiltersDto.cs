namespace Core.Contracts.Dto
{
    public record ProductFiltersDto(string? Search, Guid? CategoryId, string? SortProp, string? SortOrder);
}