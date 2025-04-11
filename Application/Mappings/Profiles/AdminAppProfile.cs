using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;

namespace Application.Mappings.Profiles
{
    public class AdminAppProfile : Profile
    {
        public AdminAppProfile()
        {
            CreateMap<AdminCreateMessage, AdminCreateDto>();
        }
    }
}