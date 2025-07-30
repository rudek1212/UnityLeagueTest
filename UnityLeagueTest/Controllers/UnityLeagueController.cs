using Microsoft.AspNetCore.Mvc;

namespace UnityLeagueTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnityLeagueController : ControllerBase
    {
        private readonly IUnityLeagueClient _unityLeagueClient;

        public UnityLeagueController(IUnityLeagueClient unityLeagueClient)
        {
            _unityLeagueClient = unityLeagueClient;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _unityLeagueClient.GetCurrentUserAsync());
        }

        [HttpGet]
        [Route("GetOrganizer")]
        public async Task<IActionResult> GetOrganizer()
        {
            return Ok(await _unityLeagueClient.GetCurrentOrganizerAsync());
        }

        [HttpGet]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            return Ok(await _unityLeagueClient.GetBearerTokenAsync());
        }

        [HttpGet]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent()
        {
            return Ok(await _unityLeagueClient.CreateEventAsync());
        }
    }
}
