using AutoMapper;
using Core.Entities;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class CategoryModelToEntityConverter : ITypeConverter<Category, CategoryEntity>
    {
        public CategoryEntity Convert(Category source, CategoryEntity destination, ResolutionContext context)
        {
            return new CategoryEntity()
            {
                Id = source.Id,
                Title = source.Title,
                ParentCategoryId = source.ParentCategoryId,
                //ParentCategory = null,
                //Subcategories = new List<CategoryEntity>()
            };
        }
    }
}