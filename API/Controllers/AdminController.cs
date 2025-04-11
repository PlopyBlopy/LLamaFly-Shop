using API.Interfaces;
using API.Utillities;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/profiles/admins")]
    public class AdminController : ControllerBase, IAdminController
    {
        private readonly IAdminService _service;
        private readonly IErrorFactoryHandler _errorFactoryHandler;
        private readonly IHttpContextTokenReader _httpContextTokenReader;

        public AdminController(IAdminService service, IErrorFactoryHandler errorFactoryHandler, IHttpContextTokenReader httpContextTokenReader)
        {
            _service = service;
            _errorFactoryHandler = errorFactoryHandler;
            _httpContextTokenReader = httpContextTokenReader;
        }

        public Task<ActionResult<AdminDto>> Get(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(AdminUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        //[Authorize(Policy = "admin")]
        //[HttpGet]
        //public async Task<ActionResult<AdminDto>> GetAdminProfile(CancellationToken ct)
        //{
        //    var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        //    if (authHeader != null && authHeader.StartsWith("Bearer "))
        //    {
        //        var profileId = await _httpContextTokenReader.SubReared(authHeader);

        //        if (profileId.IsFailed)
        //            return BadRequest(profileId.Errors);

        //        var result = await _service.Get(profileId.Value, ct);

        //        return result.IsSuccess
        //            ? Ok(result)
        //            : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        //    }

        //    return BadRequest("Invalid token");
        //}

        //[Authorize(Policy = "admin")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateAdminProfile(AdminProfileUpdateRequest request, CancellationToken ct)
        //{
        //    var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        //    if (authHeader != null && authHeader.StartsWith("Bearer "))
        //    {
        //        var profileId = await _httpContextTokenReader.SubReared(authHeader);

        //        if (profileId.IsFailed)
        //            return BadRequest(profileId.Errors);

        //        var result = await _service.Update(profileId.Value, request, ct);

        //        return result.IsSuccess
        //            ? Ok(result)
        //            : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        //    }

        //    return BadRequest("Invalid token");
        //}

        //[Authorize(Policy = "admin")]
        //[HttpDelete]
        //public async Task<ActionResult> DeleteAdminProfile(CancellationToken ct)
        //{
        //    var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        //    if (authHeader != null && authHeader.StartsWith("Bearer "))
        //    {
        //        var profileId = await _httpContextTokenReader.SubReared(authHeader);

        //        if (profileId.IsFailed)
        //            return BadRequest(profileId.Errors);

        //        var result = await _service.Delete(profileId.Value, ct);

        //        return result.IsSuccess
        //            ? Ok(result)
        //            : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        //    }

        //    return BadRequest("Invalid token");
        //}
    }
}