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
    IJwtService _jwtService,
    IEmailService _emailService
    ) : IAuthService
    {
        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO credentials)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(credentials.Email);
            if (existingUser != null)
                throw new InvalidOperationException("L'email est déjà utilisée");

            // TODO: générer mot de passe aléatoire
            var generatedPassword = "TODO";

            // TODO: hacher le mot de passe
            var hashedPassword = _passwordHasherService.HashPassword(generatedPassword);

            var user = new User
            {
                Email = credentials.Email,
                Password = hashedPassword //To do

            };

            await _userRepository.AddAsync(user);

            // TODO: envoyer email avec mot de passe provisoire
            await _emailService.SendPasswordAsync(user.Email, generatedPassword);

            return new RegisterResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
            };
        }
    }
}
