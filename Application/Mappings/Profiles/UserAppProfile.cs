using Application.Mappings.Converters;
using AutoMapper;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Profiles
{
    public class UserAppProfile : Profile
    {
        public UserAppProfile()
        {
            CreateMap<UserRegisterDto, User>().ConvertUsing<UserRegisterDtoToModelConverter>();
            CreateMap<User, UserProfileDto>().ConvertUsing<UserModelToUserProfileConverter>();
        }
    }
}