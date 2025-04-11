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
    public class SellerService : ISellerService

    {
        private readonly ISellerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProfileCreateMessage> _profileValidator;

        public SellerService(ISellerRepository repository, IMapper mapper, IValidator<ProfileCreateMessage> profileValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _profileValidator = profileValidator;
        }

        public async Task<Result<Guid>> AddConsumerProfileMessage<T>(T dto, CancellationToken ct = default)
        {
            var profile = dto as UserProfileSellerCreateMessage;

            var profileValidationResult = _profileValidator.Validate(profile.Profile);
            //ALERT: Validate Seller

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
            var sellerDto = _mapper.Map<SellerCreateDto>(profile.Seller);
            UserProfileSellerCreateDto createDto = new UserProfileSellerCreateDto(userDto, profileDto, sellerDto);

            return await _repository.AddConsumerSeller(createDto, ct);
        }

        public async Task<Result<SellerDto>> GetSeller(Guid id, CancellationToken ct)
        {
            return await _repository.GetSeller(id, ct);
        }

        public Task<Result> UpdateSeller(Guid id, SellerUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> IsExist(Guid id, CancellationToken ct)
        {
            return _repository.IsExist(id, ct);
        }
    }
}