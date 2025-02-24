using AutoMapper;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class SellerDataToSellerProfileConverter : ITypeConverter<SellerRegisterDto, SellerProfileDto>
    {
        public SellerProfileDto Convert(SellerRegisterDto source, SellerProfileDto destination, ResolutionContext context)
        {
            return new SellerProfileDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}