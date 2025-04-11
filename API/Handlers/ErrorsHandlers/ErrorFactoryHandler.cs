using API.Interfaces;
using Core.Extensions.Errors;
using FluentResults;

namespace API.Handlers.ErrorsHandlers
{
    public class ErrorFactoryHandler : IErrorFactoryHandler
    {
        private readonly IWebHostEnvironment _env;

        public ErrorFactoryHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IErrorHandler GetHandler(IError error)
        {
            return error switch
            {
                ValidationError => new ValidationErrorHandler(),
                ValidationRangeError => new ValidationRangeErrorHandler(),
                NotFoundError => new NotFoundErrorHandler(),
                NotNullError => new NotNullErrorHandler(),
                _ => new DefaultErrorHandler(_env)
            };
        }
    }
}