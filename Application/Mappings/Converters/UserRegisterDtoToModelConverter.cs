using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class UserRegisterDtoToModelConverter : ITypeConverter<UserRegisterDto, User>
    {
        private readonly IPasswordHasher _hasher;

        public UserRegisterDtoToModelConverter(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }

        public User Convert(UserRegisterDto source, User destination, ResolutionContext context)
        {
            string hashingPassword = _hasher.Hashing(source.Password);

            return new User(Guid.NewGuid(), source.Role, source.Login, source.Email, source?.PhoneNumber, hashingPassword);
        }
    }
}