using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Requests;
using Core.Models;

namespace Application.Mappings.Profiles
{
    public class CategoryAppicationProfile : Profile
    {
        public CategoryAppicationProfile()
        {
            CreateMap<CategoryCreateRequest, Category>().ConvertUsing<CategoryCreateRequestToModelConverter>();
            CreateMap<CategoryUpdateRequest, CategoryUpdateDto>().ConvertUsing<CategoryUpdateRequestToUpdateDtoConverter>();
        }
    }
}