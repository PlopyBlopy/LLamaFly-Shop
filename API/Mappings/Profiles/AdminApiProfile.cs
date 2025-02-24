using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Profiles
{
    public class AdminApiProfile : Profile
    {
        public AdminApiProfile()
        {
            CreateMap<AdminRegisterRequest, AdminRegisterDto>().ConvertUsing<AdminRegisterRequestToRegisterDtoConverter>();
        }
    }
}