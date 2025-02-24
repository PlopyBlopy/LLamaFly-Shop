using AutoMapper;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class CategoryCreateDtoToModelConverter : ITypeConverter<CategoryCreateDto, Category>
    {
        public Category Convert(CategoryCreateDto source, Category destination, ResolutionContext context)
        {
            return new Category(Guid.NewGuid(), source.Title, source.ParentCategoryId, new List<Category>()); //new List<Guid>()
        }
    }
}