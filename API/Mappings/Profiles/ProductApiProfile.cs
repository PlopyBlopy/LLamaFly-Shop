using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Responses;
using Core.Contracts.Dtos;

namespace API.Mappings.Profiles
{
    public class ProductApiProfile : Profile
    {
        public ProductApiProfile()
        {
            CreateMap<ProductCreateRequest, ProductCreateDto>().ConvertUsing<ProductCreateRequestToCreateDtoConverter>();
            CreateMap<ProductFiltersRequest, ProductFiltersDto>();
            CreateMap<ProductUpdateRequest, ProductUpdateDto>().ConvertUsing<ProductUpdateRequestToUpdateDtoConverter>();

            CreateMap<ProductDto, ProductResponse>().ConvertUsing<ProductDtoToResponseConverter>();
            CreateMap<ProductSellerDto, ProductSellerResponse>().ConvertUsing<ProductSellerDtoToSellerResponseConverter>();
        }
    }
}