using AutoMapper;
using Core.Contracts.Dtos;
using Application.Mappings.Converters;

namespace Application.Mappings.Profiles
{
    public class AdminAppProfile : Profile
    {
        public AdminAppProfile()
        {
            CreateMap<AdminRegisterDto, AdminProfileDto>().ConvertUsing<AdminRegisterDtoToProfileDtoConverter>();
        }
    }
}