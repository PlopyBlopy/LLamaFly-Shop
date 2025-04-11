using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    public class UserLoginRequestToLoginDtoConverter : ITypeConverter<UserLoginRequest, UserLoginDto>
    {
        public UserLoginDto Convert(UserLoginRequest source, UserLoginDto destination, ResolutionContext context)
        {
            string identifier = null!;

            if (source.Login != null)
                identifier = source.Login;
            else if (source.Email != null)
                identifier = source.Email;
            else if (source.PhoneNumber != null)
                identifier = source.PhoneNumber;

            return new UserLoginDto(identifier, source.password);
        }
    }
}