using Microsoft.AspNetCore.Mvc;
using BrewingBuddies_RiotClient;

namespace BrewingBuddies.Controllers
{
    //Move to the BLL layer

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        string apiKey = "RGAPI-15fa1a15-1495-42fe-a2da-496fa8026bf3";

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
