using Application.Validation.Models;
using Core.Contracts.Requests;
using FluentResults;
using FluentValidation;

namespace Application.Utillities
{
    public class ValidatorFactory : IValidatorFactory
    {
        private static readonly Dictionary<Type, Func<IValidator>> _validators = new()
        {
            { typeof(ProfileUpdateRequest), () => new ProfileUpdateRequestValidator() }
        };

        public async Task<Result<AbstractValidator<T>>> GetValidator<T>()
        {
            if (_validators.TryGetValue(typeof(T), out var validatorFactory))
            {
                var validator = (AbstractValidator<T>)validatorFactory();
                return Result.Ok(validator);
            }

            return Result.Fail<AbstractValidator<T>>("Validator not found for this type");
        }
    }

    public interface IValidatorFactory
    {
        Task<Result<AbstractValidator<T>>> GetValidator<T>();
    }
}