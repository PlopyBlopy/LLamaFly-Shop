using AutoMapper;
using Application.Mappings.Converters;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Models;

namespace Application.Mappings.Profiles
{
    public class ProductApplicationProfile : Profile
    {
        public ProductApplicationProfile()
        {
            CreateMap<ProductCreateDto, Product>().ConvertUsing<ProductCreateDtoToModelConverter>();
            CreateMap<Product, ProductEntity>().ConvertUsing<ProductModelToEntityConverter>();
        }
    }
}