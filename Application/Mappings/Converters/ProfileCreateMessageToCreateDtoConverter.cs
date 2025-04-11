using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    internal class ProfileCreateMessageToCreateDtoConverter : ITypeConverter<ProfileUpdateRequest, ProfileUpdateDto>
    {
        public ProfileUpdateDto Convert(ProfileUpdateRequest source, ProfileUpdateDto destination, ResolutionContext context)
        {
            Guid id = (Guid)context.Items["id"];
            return new ProfileUpdateDto(id, source.Name, source.Surname, source.Patronymic, DateTime.Now);
        }
    }
}