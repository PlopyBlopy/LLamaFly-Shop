using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Messages;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using Core.Models;
using FluentResults;
using static Core.Interfaces.Constraints.IUserConstraints;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IKafkaProducer<UserProfileAdminCreateMessage> _producerAdmin;
        private readonly IKafkaProducer<UserProfileSellerCreateMessage> _producerSeller;
        private readonly IKafkaProducer<UserProfileCustomerCreateMessage> _producerCustomer;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserService userService, IUserRepository userRepository, IMapper mapper, IKafkaProducer<UserProfileAdminCreateMessage> producerAdmin, IKafkaProducer<UserProfileSellerCreateMessage> producerSeller, IKafkaProducer<UserProfileCustomerCreateMessage> producerCustomer, IPasswordHasher hasher, ITokenService tokenService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
            _producerAdmin = producerAdmin;
            _producerSeller = producerSeller;
            _producerCustomer = producerCustomer;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<Result<TokensDto>> Login(UserLoginRequest request, CancellationToken ct)
        {
            var dto = _mapper.Map<UserLoginDto>(request);

            var model = await _userRepository.Get(dto.Identifier, ct);

            if (model.IsFailed)
            {
                return Result.Fail(model.Errors);
            }

            bool passwordVerify = _hasher.Verify(dto.Password, model.Value.Password);

            if (!passwordVerify)
            {
                return Result.Fail(new InvalidError("Password", "Password"));
            }

            string accessToken = _tokenService.GenerateAccessToken(model.Value);
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken(model.Value);

            await _tokenService.Add(refreshToken, ct);

            return new TokensDto(accessToken, refreshToken);
        }

        //ALERT: Дублирование кода для RegisterAdmin, RegisterSeller, RegisterCustomer
        public async Task<Result> RegisterAdmin(UserProfileAdminRegisterRequest request, CancellationToken ct)
        {
            var userMessage = await _userService.AddUser(UserRole.admin, request.User, ct);

            if (userMessage.IsFailed)
                return Result.Fail(userMessage.Errors);

            var profileMessage = _mapper.Map<ProfileCreateMessage>(request.Profile, opt =>
            {
                opt.Items.Add("CreatedAt", userMessage.Value.CreatedAt);
                opt.Items.Add("UpdatedAt", userMessage.Value.UpdatedAt);
            });

            var adminMessage = _mapper.Map<AdminCreateMessage>(request.Admin, opt =>
            {
                opt.Items.Add("Id", profileMessage.Id);
                opt.Items.Add("CreatedAt", profileMessage.CreatedAt);
                opt.Items.Add("UpdatedAt", profileMessage.UpdatedAt);
            });

            UserProfileAdminCreateMessage message = new UserProfileAdminCreateMessage(userMessage.Value, profileMessage, adminMessage);

            await _producerAdmin.ProduceAsync(message.User.Id.ToString(), message, ct);

            return Result.Ok();
        }

        public async Task<Result> RegisterSeller(UserProfileSellerRegisterRequest request, CancellationToken ct)
        {
            var userMessage = await _userService.AddUser(UserRole.seller, request.User, ct);

            if (userMessage.IsFailed)
                return Result.Fail(userMessage.Errors);

            var profileMessage = _mapper.Map<ProfileCreateMessage>(request.Profile, opt =>
            {
                opt.Items.Add("CreatedAt", userMessage.Value.CreatedAt);
                opt.Items.Add("UpdatedAt", userMessage.Value.UpdatedAt);
            });

            var sellerMessage = _mapper.Map<SellerCreateMessage>(request.Seller, opt =>
            {
                opt.Items.Add("Id", profileMessage.Id);
                opt.Items.Add("CreatedAt", profileMessage.CreatedAt);
                opt.Items.Add("UpdatedAt", profileMessage.UpdatedAt);
            });

            UserProfileSellerCreateMessage message = new UserProfileSellerCreateMessage(userMessage.Value, profileMessage, sellerMessage);

            await _producerSeller.ProduceAsync(message.User.Id.ToString(), message, ct);

            return Result.Ok();
        }

        public async Task<Result> RegisterCustomer(UserProfileCustomerRegisterRequest request, CancellationToken ct)
        {
            var userMessage = await _userService.AddUser(UserRole.customer, request.User, ct);

            if (userMessage.IsFailed)
                return Result.Fail(userMessage.Errors);

            var profileMessage = _mapper.Map<ProfileCreateMessage>(request.Profile, opt =>
            {
                opt.Items.Add("CreatedAt", userMessage.Value.CreatedAt);
                opt.Items.Add("UpdatedAt", userMessage.Value.UpdatedAt);
            });

            var customerMessage = _mapper.Map<CustomerCreateMessage>(request.Customer, opt =>
            {
                opt.Items.Add("Id", profileMessage.Id);
                opt.Items.Add("CreatedAt", profileMessage.CreatedAt);
                opt.Items.Add("UpdatedAt", profileMessage.UpdatedAt);
            });

            UserProfileCustomerCreateMessage message = new UserProfileCustomerCreateMessage(userMessage.Value, profileMessage, customerMessage);

            await _producerCustomer.ProduceAsync(message.User.Id.ToString(), message, ct);

            return Result.Ok();
        }
    }
}