using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Profiles
{
    public class TokenApiProfile : Profile
    {
        public TokenApiProfile()
        {
            CreateMap<RefreshTokenRequest, RefreshTokenDto>();
            CreateMap<string, RefreshTokenDto>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src));
        }
    }
}