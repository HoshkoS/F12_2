using System.Text;
using Domain.Dtos.UserDtos;
using Domain.Models;
using Domain.Services.UserService;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using ILogger = Serilog.ILogger;

namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
    private readonly UserRepository userRepository;
    private readonly ILogger _logger;
    private readonly ServerDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(ServerDbContext context, ILogger logger, IHttpContextAccessor httpContext)
    {
        _context = context;
        userRepository = new UserRepository(_context);
        _logger = logger;
        _httpContextAccessor = httpContext;
    }

    public async Task<User> createUser(RegisterUserDto user)
    {
        if (user.Password != user.PasswordRepeat)
        {
            _logger.Error($"\'Password\' and \'Repeat Password\' fields must be the same!");
            throw new Exception("Password and Repeat Password fields must be the same!");
        }

        var isUserExist = await userRepository.FirstOrDefault(u => u.Email == user.Email);
        if (isUserExist != null)
        {
            _logger.Error(isUserExist.ToString());
            _logger.Error($"User with Email {user.Email} already exists!");
            throw new Exception($"User with Email {user.Email} already exists!");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = new User
        {
            Email = user.Email,
            Password = hashedPassword,
            Name = user.Name,
            Surname = user.Surname,
            Currency = user.Currency,
            BirthDate = DateTime.Parse(user.Birthdate)
        };
        await userRepository.Add(newUser);
        return await Task.FromResult(newUser);
    }

    public async Task<bool> checkUser(LoginUserDto user)
    {
        var userData = await userRepository.FirstOrDefault(u => u.Email == user.Email);
        if (userData == null)
        {
            _logger.Error($"\'User\' not found!");
            throw new Exception("User not found!");
        }

        var verified = BCrypt.Net.BCrypt.Verify(user.Password, userData.Password);
        if (!verified)
        {
            _logger.Error($"\'Password\' is wrong!");
            throw new Exception("Password is wrong!");
        }

        var userIdString = userData.Id.ToString();
        var userIdBytes = Encoding.UTF8.GetBytes(userIdString);
        _httpContextAccessor.HttpContext.Session.Set("UserId", userIdBytes);

        return await Task.FromResult(true);
    }

    public async Task<UserDto> GetUser(Guid id)
    {
        try
        {
            var user = (await userRepository.Find(u => u.Id == id)).SingleOrDefault();
            if (user == null)
            {
                _logger.Error($"User with ID {id} not found.");
                throw new Exception($"User with ID {id} not found.");
            }

            var userDto = new UserDto(user);

            return userDto;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while getting user with ID {id}: {ex.Message}");
            throw;
        }
    }
}