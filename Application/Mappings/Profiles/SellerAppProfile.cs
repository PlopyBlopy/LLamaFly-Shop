using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;

namespace Application.Mappings.Profiles
{
    public class SellerAppProfile : Profile
    {
        public SellerAppProfile()
        {
            CreateMap<SellerCreateMessage, SellerCreateDto>();
        }
    }
}