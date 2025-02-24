using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Profiles
{
    public class CustomerApiProfile : Profile
    {
        public CustomerApiProfile()
        {
            CreateMap<CustomerRegisterRequest, CustomerRegisterDto>().ConvertUsing<CustomerRegisterRequestToRegisterDtoConverter>();
        }
    }
}