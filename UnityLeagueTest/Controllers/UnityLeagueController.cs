using Microsoft.AspNetCore.Mvc;
using UnityLeagueTest.Transfer.Dto;
using UnityLeagueTest.Transfer.Request;

namespace UnityLeagueTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnityLeagueController : ControllerBase
    {
        private readonly IUnityLeagueClient _unityLeagueClient;

        public UnityLeagueController(IUnityLeagueClient unityLeagueClient)
        {
            _unityLeagueClient = unityLeagueClient;
        }

        /// <summary>
        /// Pobiera szczegó³y wydarzenia po ID
        /// </summary>
        [HttpGet("events/{id:int}")]
        public async Task<ActionResult<EventDetailsDto>> GetEventById(int id, CancellationToken cancellationToken)
        {
            var ev = await _unityLeagueClient.GetEventByIdAsync(id, cancellationToken);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        /// <summary>
        /// Tworzy nowe wydarzenie
        /// </summary>
        [HttpPost("events")]
        public async Task<ActionResult<EventDetailsDto>> CreateEvent([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            var ev = await _unityLeagueClient.CreateEventAsync(request, cancellationToken);
            return CreatedAtAction(nameof(CreateEvent), new { id = ev?.Id }, ev);
        }

        /// <summary>
        /// Aktualizuje wydarzenie
        /// </summary>
        [HttpPut("events/{id:int}")]
        public async Task<ActionResult<EventDetailsDto>> UpdateEvent(int id, [FromBody] UpdateEventRequest request, CancellationToken cancellationToken)
        {
            var ev = await _unityLeagueClient.UpdateEventAsync(id, request, cancellationToken);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        /// <summary>
        /// Usuwa wydarzenie
        /// </summary>
        [HttpDelete("events/{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            var success = await _unityLeagueClient.DeleteEventAsync(id, cancellationToken);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Raportuje wyniki turnieju
        /// </summary>
        [HttpPatch("events/{id:int}")]
        public async Task<IActionResult> ReportResults(int id, [FromBody] ReportResultsRequest request, CancellationToken cancellationToken)
        {
            var ev = await _unityLeagueClient.ReportResultsAsync(id, request, cancellationToken);
            if (ev == null)
                return BadRequest("Nie uda³o siê przes³aæ wyników.");

            return Ok(new { message = "Wyniki przes³ane pomyœlnie." });
        }
    }
}
