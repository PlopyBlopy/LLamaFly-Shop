using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Profiles
{
    public class AdminAppProfile : Profile
    {
        public AdminAppProfile()
        {
            CreateMap<AdminRegisterRequest, AdminCreateMessage>().ConvertUsing<AdminRegisterRequestToCreateMessageConverter>();
        }
    }
}