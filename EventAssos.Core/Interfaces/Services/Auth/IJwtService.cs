using EventAssos.Core.DTOs.Responses;
using EventAssos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Auth
{
    public interface IJwtService
    {
        Task<LoginResponseDto> GenerateToken(User user);
    }
}
