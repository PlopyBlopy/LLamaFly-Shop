using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;

namespace Application.Mappings.Profiles
{
    public class SellerAppProfile : Profile
    {
        public SellerAppProfile()
        {
            CreateMap<SellerRegisterDto, SellerProfileDto>().ConvertUsing<SellerDataToSellerProfileConverter>();
        }
    }
}