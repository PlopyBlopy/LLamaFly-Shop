using AutoMapper;
using Core.Contracts.Dtos;
using Application.Mappings.Converters;

namespace Application.Mappings.Profiles
{
    public class CustomerAppProfile : Profile
    {
        public CustomerAppProfile()
        {
            CreateMap<CustomerRegisterDto, CustomerProfileDto>().ConvertUsing<CustomerRegisterDtoToProfileDtoConverter>();
        }
    }
}