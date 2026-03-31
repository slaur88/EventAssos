using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.DTOs.Responses;
using EventAssos.Secu.Interfaces.Repositories;
using EventAssos.Secu.Interfaces.Services.Auth;
using EventAssos.Secu.Interfaces.Services.Tools;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Services.Auth;

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
        // On cherche l'utilisateur par l'identifiant fourni (mail ou pseudo)
        var user = await _userRepository.GetByEmailOrPseudoAsync(credentials.Identifier);

        // Si l'utilisateur n'existe pas ou que le mot de passe est faux
        if (user == null || !_passwordHasherService.VerifyPassword(credentials.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Identifiant ou mot de passe incorrect");
        }

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


        string subject = "Bienvenue - Votre mot de passe provisoire";

        //Corps du message 
        string body = $@"
            <html>
                <body>
                    <h2>Bienvenue sur EventAssos !</h2>
                    <p>Votre compte a été créé avec succès.</p>
                    <p>Votre identifiant: <strong>{user.Email}</strong></p>
                    <p>Voici votre mot de passe provisoire : <strong>{generatedPassword}</strong></p>
                    <p>Nous vous conseillons de le modifier dès votre première connexion.</p>
                </body>
            </html>";

        
        await _emailService.SendEmailAsync(user.Email, subject, body);

        return new RegisterResponseDTO
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
}
