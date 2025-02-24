using AutoMapper;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class AdminRegisterDtoToProfileDtoConverter : ITypeConverter<AdminRegisterDto, AdminProfileDto>
    {
        public AdminProfileDto Convert(AdminRegisterDto source, AdminProfileDto destination, ResolutionContext context)
        {
            return new AdminProfileDto(source.Surname, source.Name, source.Patronymic);
        }
    }
}