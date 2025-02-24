using AutoMapper;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class CustomerRegisterDtoToProfileDtoConverter : ITypeConverter<CustomerRegisterDto, CustomerProfileDto>
    {
        public CustomerProfileDto Convert(CustomerRegisterDto source, CustomerProfileDto destination, ResolutionContext context)
        {
            return new CustomerProfileDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}