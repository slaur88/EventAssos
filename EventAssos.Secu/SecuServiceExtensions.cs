using EventAssos.Secu.Interfaces.Repositories;
using EventAssos.Secu.Interfaces.Services.Auth;
using EventAssos.Secu.Interfaces.Services.Data;
using EventAssos.Secu.Interfaces.Services.Tools;
using EventAssos.Secu.Services.Auth;
using EventAssos.Secu.Services.Data;
using EventAssos.Secu.Services.Tools;
using EventAssos.Secu.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu;

public static class SecuServiceExtensions
{
    public static IServiceCollection AddSecuServices(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        // Settings email
        var emailSettings = configuration
            .GetSection("EmailSettings")
            .Get<EmailSettings>();
        services.AddSingleton(emailSettings);


        //Settings Jwt
        var jwtSettings = configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>();
        services.AddSingleton(jwtSettings);

        // Services 
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        

        return services;
    }
}
