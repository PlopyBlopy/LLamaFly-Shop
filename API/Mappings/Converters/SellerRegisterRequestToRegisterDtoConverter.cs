using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Converters
{
    public class SellerRegisterRequestToRegisterDtoConverter : ITypeConverter<SellerRegisterRequest, SellerRegisterDto>
    {
        public SellerRegisterDto Convert(SellerRegisterRequest source, SellerRegisterDto destination, ResolutionContext context)
        {
            return new SellerRegisterDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}