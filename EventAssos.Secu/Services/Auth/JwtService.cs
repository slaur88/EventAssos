using EventAssos.Secu.DTOs.Responses;
using EventAssos.Secu.Interfaces.Services.Auth;
using EventAssos.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventAssos.Secu.Services.Auth
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public Task<LoginResponseDto> GenerateToken(User user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is not configured.");
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "30"));

            // Création des claims (informations sur l'utilisateur)
            var claims = new[]
            {
            // Claims standard
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),

            // Claims personnalisés
            new Claim("role", user.Role.ToString())
        };

            // Génération du JWT Token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // HmacSha256 a besoin de 256 bits (32 caractères)

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            // Génération du token sous forme de chaîne
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(new LoginResponseDto
            {
                Token = tokenString,
                Expiration = expiration
            });
        }
    }
}
