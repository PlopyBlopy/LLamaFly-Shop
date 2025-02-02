using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dto;
using API.Contracts.Requests;
using API.Contracts.Responses;

namespace API.Mappings.Profiles
{
    public class ProductApiProfile : Profile
    {
        public ProductApiProfile()
        {
            CreateMap<ProductCreateRequest, ProductCreateDto>();
            CreateMap<ProductFiltersRequest, ProductFiltersDto>();

            CreateMap<ProductDto, ProductResponse>().ConvertUsing<ProductDtoToProductResponseConverter>();
        }
    }
}