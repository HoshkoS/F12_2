using Domain.Dtos.UserDtos;
using Domain.Models;

namespace Domain.Services.UserService
{
    public interface IUserService
    {
        Task<User> createUser(RegisterUserDto user);
        Task<bool> checkUser(LoginUserDto user);

        UserDto getUser(Guid id);
    }
}
