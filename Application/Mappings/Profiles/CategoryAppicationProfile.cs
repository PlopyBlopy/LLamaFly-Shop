using AutoMapper;
using Application.Mappings.Converters;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Models;

namespace Application.Mappings.Profiles
{
    public class CategoryAppicationProfile : Profile
    {
        public CategoryAppicationProfile()
        {
            CreateMap<CategoryCreateDto, Category>().ConvertUsing<CategoryCreateDtoToModelConverter>();
            CreateMap<Category, CategoryEntity>().ConvertUsing<CategoryModelToEntityConverter>();
        }
    }
}