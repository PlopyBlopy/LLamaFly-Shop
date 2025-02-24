using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public AuthController(IUserService service, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest request, CancellationToken ct)
        {
            // TODO: validation

            var dto = _mapper.Map<UserLoginDto>(request);

            string token = await _service.Login(dto, ct);

            return Ok(new { Token = token });
        }

        //ALERT: Дублирование кода для RegisterAdmin, RegisterSeller, RegisterCustomer
        [Authorize(Policy = "Admin")]
        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] UserAdminRegisterRequest request, CancellationToken ct)
        {
            // TODO: validation User

            var user = _mapper.Map<UserRegisterDto>(request.User);
            var admin = _mapper.Map<AdminRegisterDto>(request.Admin);

            UserAdminRegisterDto dto = new UserAdminRegisterDto(user, admin);

            await _service.RegisterAdmin(dto, ct);

            return Ok();
        }

        [HttpPost("register-seller")]
        public async Task<ActionResult> RegisterSeller([FromBody] UserSellerRegisterRequest request, CancellationToken ct)
        {
            // TODO: validation User

            var user = _mapper.Map<UserRegisterDto>(request.User);
            var seller = _mapper.Map<SellerRegisterDto>(request.Seller);

            UserSellerRegisterDto dto = new UserSellerRegisterDto(user, seller);

            await _service.RegisterSeller(dto, ct);

            return Ok();
        }

        [HttpPost("register-customer")]
        public async Task<ActionResult> RegisterCustomer([FromBody] UserCustomerRegisterRequest request, CancellationToken ct)
        {
            // TODO: validation User

            var user = _mapper.Map<UserRegisterDto>(request.User);
            var customer = _mapper.Map<CustomerRegisterDto>(request.Customer);

            UserCustomerRegisterDto dto = new UserCustomerRegisterDto(user, customer);

            await _service.RegisterCustomer(dto, ct);

            return Ok();
        }
    }
}