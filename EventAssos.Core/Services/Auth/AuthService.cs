using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.DTOs.Responses;
using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Services.Auth
{
    public class AuthService(
    IUserRepository _userRepository,
    IPasswordHasherService _passwordHasherService,
    IPasswordGeneratorService _passwordGeneratorService,
    IJwtService _jwtService,
    IEmailService _emailService
    ) : IAuthService
    {
        public async Task<LoginResponseDto> Login(LoginRequestDTO credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
                throw new ArgumentException("Email et mot de passe sont requis");

            var user = await _userRepository.GetUserByEmailAsync(credentials.Email);
            if (user == null || !_passwordHasherService.VerifyPassword(credentials.Password, user.Password))
                throw new UnauthorizedAccessException("Email ou mot de passe incorrect");

            return await _jwtService.GenerateToken(user);
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO credentials)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(credentials.Email);
            if (existingUser != null)
                throw new InvalidOperationException("L'email est déjà utilisée");


            var generatedPassword = _passwordGeneratorService.RandomPassword();


            var hashedPassword = _passwordHasherService.HashPassword(generatedPassword);

            var user = new User
            {
                Email = credentials.Email,
                Password = hashedPassword 

            };

            await _userRepository.AddAsync(user);

            
            await _emailService.SendEmailAsync(user.Email, generatedPassword);

            return new RegisterResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
            };
        }
    }
}
