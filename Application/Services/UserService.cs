using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IKafkaProducer<UserAdminProfileDto> _producerAdmin;
        private readonly IKafkaProducer<UserSellerProfileDto> _producerSeller;
        private readonly IKafkaProducer<UserCustomerProfileDto> _producerCustomer;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        //ALERT: Дублирование кода для producerAdmin, producerSeller, producerCustomer
        public UserService(IUserRepository repository, IMapper mapper, IKafkaProducer<UserAdminProfileDto> producerAdmin, IKafkaProducer<UserSellerProfileDto> producerSeller, IKafkaProducer<UserCustomerProfileDto> producerCustomer, IPasswordHasher hasher, ITokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _producerAdmin = producerAdmin;
            _producerSeller = producerSeller;
            _producerCustomer = producerCustomer;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<string> Login(UserLoginDto dto, CancellationToken ct)
        {
            var model = await _repository.Get(dto.Identifier, ct);

            if (model == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            bool passwordVerify = _hasher.Verify(dto.Password, model.Password);
            if (!passwordVerify)
                throw new UnauthorizedAccessException("Неверный пароль");

            return _tokenService.GenerateToken(model);
        }

        //ALERT: Дублирование кода для RegisterAdmin, RegisterSeller, RegisterCustomer
        public async Task RegisterAdmin(UserAdminRegisterDto dto, CancellationToken ct)
        {
            var model = _mapper.Map<User>(dto.User);

            await _repository.Add(model, ct);

            var user = _mapper.Map<UserProfileDto>(model);
            var admin = _mapper.Map<AdminProfileDto>(dto.Admin);
            var userAdmin = new UserAdminProfileDto(user, admin);

            await _producerAdmin.ProduceAsync(userAdmin.User.id.ToString(), userAdmin, ct);
        }

        public async Task RegisterSeller(UserSellerRegisterDto dto, CancellationToken ct)
        {
            var model = _mapper.Map<User>(dto.User);

            await _repository.Add(model, ct);

            var user = _mapper.Map<UserProfileDto>(model);
            var seller = _mapper.Map<SellerProfileDto>(dto.Seller);
            var userSeller = new UserSellerProfileDto(user, seller);

            await _producerSeller.ProduceAsync(userSeller.User.id.ToString(), userSeller, ct);
        }

        public async Task RegisterCustomer(UserCustomerRegisterDto dto, CancellationToken ct)
        {
            var model = _mapper.Map<User>(dto.User);

            await _repository.Add(model, ct);

            var user = _mapper.Map<UserProfileDto>(model);
            var customer = _mapper.Map<CustomerProfileDto>(dto.Customer);
            var userCustomer = new UserCustomerProfileDto(user, customer);

            await _producerCustomer.ProduceAsync(userCustomer.User.id.ToString(), userCustomer, ct);
        }
    }
}