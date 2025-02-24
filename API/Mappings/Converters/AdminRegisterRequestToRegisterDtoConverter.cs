using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Converters
{
    public class AdminRegisterRequestToRegisterDtoConverter : ITypeConverter<AdminRegisterRequest, AdminRegisterDto>
    {
        public AdminRegisterDto Convert(AdminRegisterRequest source, AdminRegisterDto destination, ResolutionContext context)
        {
            return new AdminRegisterDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}