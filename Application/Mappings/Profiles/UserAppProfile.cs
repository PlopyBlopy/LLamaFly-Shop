using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;
using Core.Contracts.Requests;
using Core.Models;

namespace Application.Mappings.Profiles
{
    public class UserAppProfile : Profile
    {
        public UserAppProfile()
        {
            CreateMap<UserLoginRequest, UserLoginDto>().ConvertUsing<UserLoginRequestToLoginDtoConverter>();
            CreateMap<UserRegisterRequest, User>().ConvertUsing<UserRegisterRequestToModelConverter>();
            CreateMap<User, UserCreateMessage>().ConvertUsing<UserModelToCreateMessageConverter>();
        }
    }
}