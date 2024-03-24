using Domain.Services.UserService;
using Infrastructure.Repositories;
using Domain.Models;
using Domain.Dtos.UserDtos;
using ILogger = Serilog.ILogger;
using BCrypt.Net;
using Infrastructure.Database;

namespace Infrastructure.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly UserRepository userRepository;
        private readonly ILogger _logger;
        private readonly ServerDbContext _context;
        public UserService(ServerDbContext context, ILogger logger)
        {
            _context = context;
            userRepository = new UserRepository(_context);
            _logger = logger;
        }
        public async Task<User> createUser (RegisterUserDto user)
        {
            if (user.Password != user.PasswordRepeat)
            {
                _logger.Error($"\'Password\' and \'Repeat Password\' fields must be the same!");
                throw new Exception("Password and Repeat Password fields must be the same!");
            }
            //var isUserExist = this.userRepository.Find(u => u.Email == user.Email);
            //if (isUserExist != null)
            //{
            //    _logger.Error(isUserExist.ToString());
            //    _logger.Error($"User with Email {user.Email} already exists!");
            //    throw new Exception($"User with Email {user.Email} already exists!");
            //}

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User newUser = new User
            {
                Email = user.Email,
                Password = hashedPassword,
                Name = user.Name,
                Surname = user.Surname,
                Currency = "UAH"
            };
            userRepository.Add(newUser);
            return await Task.FromResult(newUser);
        }
    }
}
