using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventAssos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Seuls les utilisateurs connectés 
    public class InscriptionController(IInscriptionService _inscriptionService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] InscriptionEventRequestDTO request)
        {
            try
            {
                // On récupère l'ID de l'utilisateur à partir des Claims du Token JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("Utilisateur non identifié.");

                Guid userId = Guid.Parse(userIdClaim);

                //On appelle le service avec l'UserId sécurisé et l'EventId du DTO
                var result = await _inscriptionService.InscriptionMemberAsync(userId, request.EventId);

                // On retourne le message (Succès ou Liste d'attente)
                return Ok(new { message = result });
            }
            catch (InvalidOperationException ex)
            {
                // Event complet, date dépassée, déjà inscrit
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Erreurs imprévues
                return StatusCode(500, new { error = "Une erreur interne est survenue." });
            }
        }
    }
}
