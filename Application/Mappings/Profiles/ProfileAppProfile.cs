using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Profiles
{
    public class ProfileAppProfile : Profile
    {
        public ProfileAppProfile()
        {
            CreateMap<ProfileCreateMessage, ProfileCreateDto>();
            CreateMap<ProfileUpdateRequest, ProfileUpdateDto>().ConvertUsing<ProfileUpdateRequestToUpdateDtoConverter>();
        }
    }
}