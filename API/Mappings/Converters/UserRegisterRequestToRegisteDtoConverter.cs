using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Converters
{
    public class UserRegisterRequestToRegisteDtoConverter : ITypeConverter<UserRegisterRequest, UserRegisterDto>
    {
        public UserRegisterDto Convert(UserRegisterRequest source, UserRegisterDto destination, ResolutionContext context)
        {
            return new UserRegisterDto(source.Role, source.Login, source.Password, source.Email, source.PhoneNumber);
        }
    }
}