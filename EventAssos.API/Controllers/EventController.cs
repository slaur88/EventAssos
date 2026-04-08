using EventAssos.Domain.Entities;
using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAssos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventController(IEventService _eventService) : ControllerBase
    {
        //Post avec ResultPattern
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEventAsync([FromBody] AddEventRequestDto dto) 
        {
            var result = await _eventService.CreateEventAsync(dto);
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
       


        //Patch/Cancel avec ResultPattern
        [HttpPatch("{id}/cancel")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelAsync([FromRoute] Guid id)
        {
            
            var result = await _eventService.CancelAsync(id);

            if (result.IsFailure)
            {
                
                if (result.ErrorMessage == "Événement introuvable.")
                {
                    return NotFound(new { message = result.ErrorMessage });
                }

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        //Delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] //Seul l'admin peut delete
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            try
            {
                await _eventService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Une erreur interne est survenue." });
            }
        }

        //Patch
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid id, [FromBody] UpdateEventRequestDTO dto)
        {
            try
            {
                var updated = await _eventService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Une erreur interne est survenue." });
            }

        }
    }
}
