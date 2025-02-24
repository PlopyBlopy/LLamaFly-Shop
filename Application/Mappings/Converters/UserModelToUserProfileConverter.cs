using AutoMapper;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class UserModelToUserProfileConverter : ITypeConverter<User, UserProfileDto>
    {
        public UserProfileDto Convert(User source, UserProfileDto destination, ResolutionContext context)
        {
            return new UserProfileDto(source.Id, source.Role, source.Login, source.Email, source.PhoneNumber);
        }
    }
}