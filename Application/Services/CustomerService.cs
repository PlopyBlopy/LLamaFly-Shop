using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using FluentResults;
using FluentValidation;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProfileCreateMessage> _profileValidator;

        public CustomerService(ICustomerRepository repository, IMapper mapper, IValidator<ProfileCreateMessage> profileValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _profileValidator = profileValidator;
        }

        public async Task<Result<Guid>> AddConsumerProfileMessage<T>(T dto, CancellationToken ct = default)
        {
            var profile = dto as UserProfileCustomerCreateMessage;

            var profileValidationResult = _profileValidator.Validate(profile.Profile);
            //ALERT: Validate Customer

            if (!profileValidationResult.IsValid)
            {
                var fieldErrors = profileValidationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail<Guid>(new ValidationError("ValidationError", fieldErrors));
            }

            var userDto = _mapper.Map<UserCreateDto>(profile.User);
            var profileDto = _mapper.Map<ProfileCreateDto>(profile.Profile);
            var customerDto = _mapper.Map<CustomerCreateDto>(profile.Customer);
            UserProfileCustomerCreateDto createDto = new UserProfileCustomerCreateDto(userDto, profileDto, customerDto);

            return await _repository.AddConsumerCustomer(createDto, ct);
        }

        public Task<Result<CustomerDto>> GetCustomer(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateCustomer(Guid id, CustomerUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}