using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    public class AdminRegisterRequestToCreateMessageConverter : ITypeConverter<AdminRegisterRequest, AdminCreateMessage>
    {
        public AdminCreateMessage Convert(AdminRegisterRequest source, AdminCreateMessage destination, ResolutionContext context)
        {
            Guid profileId = (Guid)context.Items["Id"];
            DateTime createdAt = (DateTime)context.Items["CreatedAt"];
            DateTime updatedAt = (DateTime)context.Items["UpdatedAt"];

            return new AdminCreateMessage(profileId, createdAt, updatedAt);
        }
    }
}