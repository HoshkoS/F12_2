using Domain.Dtos.UserDtos;
using Domain.Models;

namespace Domain.Services.UserService;

public interface IUserService
{
    Task<User> CreateUser(RegisterUserDto user);

    Task<UserDto> GetUser(Guid id);
}