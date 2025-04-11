using FluentResults;

namespace API.Interfaces
{
    public interface IErrorFactoryHandler
    {
        IErrorHandler GetHandler(IError error);
    }
}