using AutoMapper;
using Core.Contracts.Dto;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class CategoryCreateDtoToModelConverter : ITypeConverter<CategoryCreateDto, Category>
    {
        public Category Convert(CategoryCreateDto source, Category destination, ResolutionContext context)
        {
            return new Category(Guid.NewGuid(), source.Title, source.ParentCategoryId, new List<Guid>());
        }
    }
}