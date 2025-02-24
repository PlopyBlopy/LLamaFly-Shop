using AutoMapper;
using Application.Mappings.Converters;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Profiles
{
    public class CategoryAppicationProfile : Profile
    {
        public CategoryAppicationProfile()
        {
            CreateMap<CategoryCreateDto, Category>().ConvertUsing<CategoryCreateDtoToModelConverter>();
        }
    }
}