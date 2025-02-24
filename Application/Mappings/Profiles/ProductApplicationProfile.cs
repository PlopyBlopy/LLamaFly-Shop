using AutoMapper;
using Application.Mappings.Converters;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Profiles
{
    public class ProductApplicationProfile : Profile
    {
        public ProductApplicationProfile()
        {
            CreateMap<ProductCreateDto, Product>().ConvertUsing<ProductCreateDtoToModelConverter>();
        }
    }
}