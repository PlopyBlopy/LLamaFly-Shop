using Core.Contracts.Dto;
using Core.Entities;

namespace DataBase.Utilities
{
    public class CategoryEntityToDtoRecursionConverter
    {
        public CategoryDto Convert(CategoryEntity source)
        {
            return new CategoryDto(
            source.Id,
                source.Title,
                source.ParentCategoryId,
                source.Subcategories
                    .Select(Convert).ToList()
            );
        }
    }
}