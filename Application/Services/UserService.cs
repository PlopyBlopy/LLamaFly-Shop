using AutoMapper;
using Core.Contracts.Messages;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using FluentResults;
using FluentValidation;
using static Core.Interfaces.Constraints.IUserConstraints;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserRegisterRequest> _registerValidator;

        public UserService(IUserRepository repository, IMapper mapper, IValidator<UserRegisterRequest> registerValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _registerValidator = registerValidator;
        }

        public async Task<Result<UserCreateMessage>> AddUser(UserRole role, UserRegisterRequest request, CancellationToken ct)
        {
            var validationResult = _registerValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail<UserCreateMessage>(new ValidationError("User", fieldErrors));
            }

            var model = _mapper.Map<User>(request, opt => opt.Items.Add("Role", role));

            var result = await _repository.Add(model, ct);

            if (result.IsFailed)
            {
                return result;
            }

            var message = _mapper.Map<UserCreateMessage>(model);

            return Result.Ok(message);
        }

        public Task<Result> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}