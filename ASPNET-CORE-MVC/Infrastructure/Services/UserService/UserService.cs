﻿using Domain.Services.UserService;
using Infrastructure.Repositories;
using Domain.Models;
using Domain.Dtos.UserDtos;
using ILogger = Serilog.ILogger;
using BCrypt.Net;
using Infrastructure.Database;
using Microsoft.Identity.Client;

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
            var isUserExist = userRepository.FirstOrDefault(u => u.Email == user.Email);
            if (isUserExist != null)
            {
                _logger.Error(isUserExist.ToString());
                _logger.Error($"User with Email {user.Email} already exists!");
                throw new Exception($"User with Email {user.Email} already exists!");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User newUser = new User
            {
                Email = user.Email,
                Password = hashedPassword,
                Name = user.Name,
                Surname = user.Surname,
                Currency = user.Currency,
                BirthDate = DateTime.Parse(user.Birthdate)
            };
            userRepository.Add(newUser);
            return await Task.FromResult(newUser);
        }

        public async Task<bool> checkUser(LoginUserDto user)
        {
            var userData = userRepository.FirstOrDefault(u => u.Email == user.Email);
            if (userData == null)
            {
                _logger.Error($"\'User\' not found!");
                throw new Exception("User not found!");
            }
            bool verified= BCrypt.Net.BCrypt.Verify(user.Password, userData.Password);
            if (!verified)
            {
                _logger.Error($"\'Password\' is wrong!");
                throw new Exception("Password is wrong!");
            }
            return await Task.FromResult(true);
        }

        public UserDto getUser(Guid id)
        {
            try
            {
                var user = userRepository.Find(u => u.Id == id).SingleOrDefault();
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
}
