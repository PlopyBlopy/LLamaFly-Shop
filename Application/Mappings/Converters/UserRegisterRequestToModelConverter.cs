using AutoMapper;
using Core.Contracts.Requests;
using Core.Interfaces;
using Core.Models;
using static Core.Interfaces.Constraints.IUserConstraints;

namespace Application.Mappings.Converters
{
    public class UserRegisterRequestToModelConverter : ITypeConverter<UserRegisterRequest, User>
    {
        private readonly IPasswordHasher _hasher;

        public UserRegisterRequestToModelConverter(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }

        public User Convert(UserRegisterRequest source, User destination, ResolutionContext context)
        {
            string hashingPassword = _hasher.Hashing(source.Password);
            UserRole role = (UserRole)context.Items["Role"];

            return new User(Guid.NewGuid(), role.ToString(), source.Login, source.Email, source.PhoneNumber, hashingPassword, DateTime.Now, DateTime.Now);
        }
    }
}