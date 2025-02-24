using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Converters
{
    public class CustomerRegisterRequestToRegisterDtoConverter : ITypeConverter<CustomerRegisterRequest, CustomerRegisterDto>
    {
        public CustomerRegisterDto Convert(CustomerRegisterRequest source, CustomerRegisterDto destination, ResolutionContext context)
        {
            return new CustomerRegisterDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}