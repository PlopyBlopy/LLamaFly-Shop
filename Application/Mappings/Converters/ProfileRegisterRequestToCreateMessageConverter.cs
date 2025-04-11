using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    public class ProfileRegisterRequestToCreateMessageConverter : ITypeConverter<ProfileRegisterRequest, ProfileCreateMessage>
    {
        public ProfileCreateMessage Convert(ProfileRegisterRequest source, ProfileCreateMessage destination, ResolutionContext context)
        {
            DateTime createdAt = (DateTime)context.Items["CreatedAt"];
            DateTime updatedAt = (DateTime)context.Items["UpdatedAt"];

            return new ProfileCreateMessage(Guid.NewGuid(), source.Name, source.Surname, source.Patronymic, createdAt, updatedAt);
        }
    }
}