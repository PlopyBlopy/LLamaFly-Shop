using Core.Contracts.Dtos;

namespace Core.Contracts.Dtos
{
    public record CategoryDto(Guid Id, string Title, Guid? ParentCategoryId, int Level) //, ICollection<CategoryDto> Subcategories
    {
        public ICollection<CategoryDto> Subcategories { get; set; } = new List<CategoryDto>();
    }
}
