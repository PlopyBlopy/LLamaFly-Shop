using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using FluentResults;
using FluentValidation;

namespace Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IValidator<ProfileUpdateRequest> _updateValidator;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IValidator<ProfileUpdateRequest> updateValidator, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        public async Task<Result<ProfileDto>> GetProfile(Guid id, CancellationToken ct)
        {
            return await _profileRepository.GetProfile(id, ct);
        }

        public async Task<Result> UpdateProfile(Guid id, ProfileUpdateRequest request, CancellationToken ct)
        {
            var validationResult = await _updateValidator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
            {
                var fieldErrors = validationResult.Errors
                    .Select(e => new ValidationFieldError(
                        message: e.ErrorMessage,
                        errorCode: e.ErrorCode,
                        propertyName: e.PropertyName,
                        attemptedValue: e.AttemptedValue))
                    .ToList();

                return Result.Fail(new ValidationError("ValidationError", fieldErrors));
            }

            var dto = _mapper.Map<ProfileUpdateDto>(request, opt => opt.Items.Add("id", id));

            var result = await _profileRepository.UpdateProfile(dto, ct);

            if (result.IsFailed)
            {
                return result;
            }

            return Result.Ok();
        }
    }
}