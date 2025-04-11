using Core.Contracts.Dtos;

namespace Core.Contracts.Dtos
{
    public record CategoryHierarchyDto(Guid Id, string Title, Guid? ParentCategoryId, int Level) //, ICollection<CategoryDto> Subcategories
    {
        public ICollection<CategoryHierarchyDto> Subcategories { get; set; } = new List<CategoryHierarchyDto>();
    }
}