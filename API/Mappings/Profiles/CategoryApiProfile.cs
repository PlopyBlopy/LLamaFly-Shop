using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Dtos;

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