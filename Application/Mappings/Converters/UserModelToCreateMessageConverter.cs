using AutoMapper;
using Core.Contracts.Messages;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class UserModelToCreateMessageConverter : ITypeConverter<User, UserCreateMessage>
    {
        public UserCreateMessage Convert(User source, UserCreateMessage destination, ResolutionContext context)
        {
            return new UserCreateMessage(source.Id, source.Role, source.Login, source.Email, source.PhoneNumber, source.CreatedAt, source.UpdatedAt);
        }
    }
}