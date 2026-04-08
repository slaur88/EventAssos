using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventAssos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Sécurise tout le contrôleur
    public class UserController(IUserService _userService) : ControllerBase
    {   
        //PATCH
        [HttpPatch("update-pseudo")]
        public async Task<IActionResult> UpdatePseudo([FromBody] UpdatePseudoRequestDTO request)
        {
            // 1. Extrait l'ID de l'utilisateur à partir du  JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Utilisateur non identifié dans le token.");

            var userId = Guid.Parse(userIdClaim);

            try
            {
                // 2. Appelle le service 
                await _userService.UpdatePseudo(userId, request.NewPseudo);

                return Ok(new { message = "Pseudo mis à jour avec succès !" });
            }
            catch (InvalidOperationException ex)
            {
                // Erreur métier (pseudo déjà existant)
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Erreur imprévue
                return StatusCode(500, new { error = "Une erreur interne est survenue." });
            }
        }


        //GET BY ID
        [HttpGet("{id}", Name = "GetUser")]
        [AllowAnonymous] //Pour consulter le profil créé sans token immédiatement
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        //DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                //  204 car la ressource n'existe plus
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { Error = "Une erreur interne est survenue lors de la suppression." });
            }
        }
    }
}
