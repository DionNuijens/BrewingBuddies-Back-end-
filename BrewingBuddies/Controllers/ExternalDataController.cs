using Microsoft.AspNetCore.Mvc;
using BrewingBuddies_RiotClient;

namespace BrewingBuddies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        string apiKey = "RGAPI-8185f5ab-f2a6-462e-85d8-fb4ba5a197db";

        [HttpGet("Account")]
        public async Task<IActionResult> GetPlayerData(string gameName, string tagLine)
        {
            try
            {
                var playerData = await API_Request.GetAccountInfo(gameName, tagLine, apiKey);

                return Ok(playerData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Summoner")]
        public async Task<IActionResult> GetSummoner(string gameName, string tagLine)
        {
            try
            {
                var playerData = await API_Request.GetSummoner(gameName, tagLine, apiKey);

                return Ok(playerData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }
    }
}
