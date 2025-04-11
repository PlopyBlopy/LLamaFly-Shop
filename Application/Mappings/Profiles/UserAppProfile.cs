using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;

namespace Application.Mappings.Profiles
{
    public class UserAppProfile : Profile
    {
        public UserAppProfile()
        {
            CreateMap<UserCreateMessage, UserCreateDto>();
        }
    }
}