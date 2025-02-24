using API.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Profiles
{
    public class UserApiProfile : Profile
    {
        public UserApiProfile()
        {
            CreateMap<UserLoginRequest, UserLoginDto>();
            CreateMap<UserRegisterRequest, UserRegisterDto>().ConvertUsing<UserRegisterRequestToRegisteDtoConverter>();
        }
    }
}