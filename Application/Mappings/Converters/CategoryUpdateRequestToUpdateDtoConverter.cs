using AutoMapper;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    public class CategoryUpdateRequestToUpdateDtoConverter : ITypeConverter<CategoryUpdateRequest, CategoryUpdateDto>
    {
        public CategoryUpdateDto Convert(CategoryUpdateRequest source, CategoryUpdateDto destination, ResolutionContext context)
        {
            return new CategoryUpdateDto(source.Id, source.Title, source.ParentCategoryId);
        }
    }
}