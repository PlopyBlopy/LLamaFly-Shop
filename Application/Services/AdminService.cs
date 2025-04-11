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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProfileCreateMessage> _profileValidator;

        public AdminService(IAdminRepository repository, IMapper mapper, IValidator<ProfileCreateMessage> profileValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _profileValidator = profileValidator;
        }

        public async Task<Result<Guid>> AddConsumerProfileMessage<T>(T dto, CancellationToken ct = default)
        {
            var profile = dto as UserProfileAdminCreateMessage;

            var profileValidationResult = _profileValidator.Validate(profile.Profile);
            //ALERT: Validate Admin

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
            var adminDto = _mapper.Map<AdminCreateDto>(profile.Admin);
            UserProfileAdminCreateDto createDto = new UserProfileAdminCreateDto(userDto, profileDto, adminDto);

            return await _repository.AddConsumerAdmin(createDto, ct);
        }

        public Task<Result<AdminDto>> GetAdmin(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAdmin(Guid id, AdminUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}

//public Task<Result<AdminDto>> GetProfile(Guid id, CancellationToken ct)
//{
//    var result = _repository.GetProfile(id, ct);
//    return result;
//}

//public async Task<Result> Update(Guid id, AdminProfileUpdateRequest request, CancellationToken ct)
//{
//    var validationResult = _adminProfileUpdateValidator.Validate(request);

//    if (!validationResult.IsValid)
//    {
//        var fieldErrors = validationResult.Errors
//            .Select(e => new ValidationFieldError(
//                message: e.ErrorMessage,
//                errorCode: e.ErrorCode,
//                propertyName: e.PropertyName,
//                attemptedValue: e.AttemptedValue))
//            .ToList();

//        return Result.Fail(new ValidationError("ValidationError", fieldErrors));
//    }

//    var dto = _mapper.Map<AdminProfileUpdateDto>(request, opt => opt.Items["Id"] = id);

//    var result = await _repository.UpdateProfile(dto, ct);

//    if (result.IsFailed)
//    {
//        return result;
//    }

//    //TODO: Обработка Result от AuthService
//    await _adminUpdateProducer.ProduceAsync(dto.id.ToString(), dto, ct);

//    return Result.Ok();
//}

//public async Task<Result> Delete(Guid id, CancellationToken ct)
//{
//    return await _repository.DeleteProfile(id, ct);
//}