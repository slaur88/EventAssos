using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Core.Services.Auth;
using EventAssos.Core.Services.Tools;
using EventAssos.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core
{
    public static class CoreServiceExtensions
    {
        public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            // Settings email
            var emailSettings = configuration
                .GetSection("EmailSettings")
                .Get<EmailSettings>();
            services.AddSingleton(emailSettings);

            // Services 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();

            return services;
        }
    }
}
