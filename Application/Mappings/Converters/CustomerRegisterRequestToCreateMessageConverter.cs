using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;

namespace Application.Mappings.Converters
{
    internal class CustomerRegisterRequestToCreateMessageConverter : ITypeConverter<CustomerRegisterRequest, CustomerCreateMessage>
    {
        public CustomerCreateMessage Convert(CustomerRegisterRequest source, CustomerCreateMessage destination, ResolutionContext context)
        {
            Guid profileId = (Guid)context.Items["Id"];
            DateTime createdAt = (DateTime)context.Items["CreatedAt"];
            DateTime updatedAt = (DateTime)context.Items["UpdatedAt"];

            return new CustomerCreateMessage(profileId, createdAt, updatedAt);
        }
    }
}