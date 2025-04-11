using AutoMapper;
using Core.Contracts.Requests;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class CategoryCreateRequestToModelConverter : ITypeConverter<CategoryCreateRequest, Category>
    {
        public Category Convert(CategoryCreateRequest source, Category destination, ResolutionContext context)
        {
            return new Category(Guid.NewGuid(), source.Title, source.ParentCategoryId, new List<Category>());
        }
    }
}