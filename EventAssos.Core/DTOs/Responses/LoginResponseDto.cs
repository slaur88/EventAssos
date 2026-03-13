using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.DTOs.Responses;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}
