using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IErrorHandler
    {
        ActionResult Handle(IError error);
    }
}