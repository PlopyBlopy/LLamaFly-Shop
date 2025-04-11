using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Contracts.Responses;

namespace API.Mappings.Profiles
{
    public class ProductApiProfile : Profile
    {
        public ProductApiProfile()
        {
            CreateMap<ProductCreateRequest, ProductCreateDto>().ConvertUsing<ProductCreateRequestToCreateDtoConverter>();
            CreateMap<ProductFiltersRequest, ProductFiltersDto>();
            //CreateMap<ProductUpdateRequest, ProductUpdateDto>().ConvertUsing<ProductUpdateRequestToUpdateDtoConverter>();

            CreateMap<ProductDto, ProductResponse>().ConvertUsing<ProductDtoToResponseConverter>();
        }
    }
}