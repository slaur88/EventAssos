using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO credentials);
        Task<LoginResponseDto> Login(LoginRequestDTO credentials);


    }
}
