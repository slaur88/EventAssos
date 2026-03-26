using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.DTOs.Responses;
using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EventAssos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO request)
        {
            try
            {
                var createdUser = await _authService.RegisterAsync(request);

                return CreatedAtAction(
                    actionName: "GetUser",
                    controllerName: "Users",
                    routeValues: new { id = createdUser.Id },
                    value: createdUser
                    );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var loginResponse = await _authService.Login(request);
                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }
    }
}
