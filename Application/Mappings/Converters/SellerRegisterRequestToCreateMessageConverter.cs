using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    internal class SellerRegisterRequestToCreateMessageConverter : ITypeConverter<SellerRegisterRequest, SellerCreateMessage>
    {
        public SellerCreateMessage Convert(SellerRegisterRequest source, SellerCreateMessage destination, ResolutionContext context)
        {
            Guid profileId = (Guid)context.Items["Id"];
            DateTime createdAt = (DateTime)context.Items["CreatedAt"];
            DateTime updatedAt = (DateTime)context.Items["UpdatedAt"];

            return new SellerCreateMessage(profileId, createdAt, updatedAt);
        }
    }
}