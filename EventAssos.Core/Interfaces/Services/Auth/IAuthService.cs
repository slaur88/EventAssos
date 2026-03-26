using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO credentials);
        Task<LoginResponseDto> Login(LoginRequestDTO credentials);


    }
}
