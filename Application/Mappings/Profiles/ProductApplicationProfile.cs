using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Models;

namespace Application.Mappings.Profiles
{
    public class ProductApplicationProfile : Profile
    {
        public ProductApplicationProfile()
        {
            CreateMap<ProductCreateRequest, Product>().ConvertUsing<ProductCreateRequestToModelConverter>();
            CreateMap<ProductCreateWithRatingRequest, Product>().ConvertUsing<ProductCreateWithRatingRequestToModelConverter>();
            CreateMap<ProductUpdateRequest, ProductUpdateDto>().ConvertUsing<ProductUpdateRequestToUpdateDtoConverter>();
        }
    }
}