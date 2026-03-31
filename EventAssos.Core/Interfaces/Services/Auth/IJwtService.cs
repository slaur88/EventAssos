using EventAssos.Secu.DTOs.Responses;
using EventAssos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Auth
{
    public interface IJwtService
    {
        Task<LoginResponseDto> GenerateToken(User user);
    }
}
