using API.Contracts.Requests;
using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dto;

namespace API.Mappings.Profiles
{
    public class CategoryApiProfile : Profile
    {
        public CategoryApiProfile()
        {
            CreateMap<CategoryCreateRequest, CategoryCreateDto>().ConvertUsing<CategoryCreateRequestToCreateDtoConverter>();
        }
    }
}