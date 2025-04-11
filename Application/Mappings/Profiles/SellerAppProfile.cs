using Application.Mappings.Converters;
using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Profiles
{
    public class SellerAppProfile : Profile
    {
        public SellerAppProfile()
        {
            CreateMap<SellerRegisterRequest, SellerCreateMessage>().ConvertUsing<SellerRegisterRequestToCreateMessageConverter>();
        }
    }
}