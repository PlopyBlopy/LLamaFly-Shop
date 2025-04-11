using API.Interfaces;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/profiles/customers")]
    public class CustomerController : ControllerBase, ICustomerController
    {
        private readonly ISellerService _service;

        public CustomerController(ISellerService service)
        {
            _service = service;
        }

        public Task<ActionResult<CustomerDto>> Get(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(CustomerUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        //[Authorize(Policy = "admin")]
        //[HttpPost]
        //public async Task AddProfile(UserSellerProfileDto request, CancellationToken ct)
        //{
        //    await _service.AddConsumerProfileMessage(request, ct);
        //}

        //[Authorize(Policy = "customer")]
        //[HttpGet]
        //public async Task<ActionResult<SellerDto>> Get(CancellationToken ct)
        //{
        //    var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        //    if (authHeader != null && authHeader.StartsWith("Bearer "))
        //    {
        //        var token = authHeader.Substring("Bearer ".Length).Trim();

        //        var handler = new JwtSecurityTokenHandler();

        //        var jsonToken = handler.ReadJwtToken(token);
        //        var sub = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        //        var profileId = Guid.Parse(sub);

        //        var result = await _service.Get(profileId, ct);

        //        return Ok(result);
        //    }

        //    return BadRequest("Invalid token");
        //}
    }
}