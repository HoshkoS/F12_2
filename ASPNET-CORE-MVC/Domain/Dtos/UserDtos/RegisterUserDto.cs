﻿namespace Domain.Dtos.UserDtos
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public string Birthdate { get; set; }
        public string Currency { get; set; }
    }
}