using AutoMapper;
using Core.Configs;
using Core.Contracts.Requests;
using Core.Contracts.Dtos;

namespace API.Mappings.Converters
{
    public class CategoryCreateRequestToCreateDtoConverter : ITypeConverter<CategoryCreateRequest, CategoryCreateDto>
    {
        public CategoryCreateDto Convert(CategoryCreateRequest source, CategoryCreateDto destination, ResolutionContext context)
        {
            Guid parentCategoryId;
            if (source.ParentCategoryId == null || source.ParentCategoryId == Guid.Empty || source.ParentCategoryId == CategoryGuidConfig.PLACEHOLDER_CATEGORY_GUID)
                parentCategoryId = CategoryGuidConfig.DEFAULT_CATEGORY_GUID;
            else
                parentCategoryId = source.ParentCategoryId.Value;

            return new CategoryCreateDto(source.Title, parentCategoryId);
        }
    }
}