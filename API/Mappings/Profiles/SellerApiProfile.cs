using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Profiles
{
    public class SellerApiProfile : Profile
    {
        public SellerApiProfile()
        {
            CreateMap<SellerRegisterRequest, SellerRegisterDto>().ConvertUsing<SellerRegisterRequestToRegisterDtoConverter>();
        }
    }
}