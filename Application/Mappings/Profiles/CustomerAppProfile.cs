using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Profiles
{
    public class CustomerAppProfile : Profile
    {
        public CustomerAppProfile()
        {
            CreateMap<CustomerRegisterRequest, CustomerCreateMessage>().ConvertUsing<CustomerRegisterRequestToCreateMessageConverter>();
        }
    }
}