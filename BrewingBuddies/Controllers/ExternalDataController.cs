using Microsoft.AspNetCore.Mvc;
using BrewingBuddies_RiotClient;
using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies.Controllers
{
    //Move to the BLL layer
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IRiotAPIService _riotAPIService;
        private IMapper _mapper;
        private readonly string apiKey = "RGAPI-d9084c39-ea13-46a7-92fd-04fad338dca6";
        public ValuesController(IRiotAPIService riotAPIService, IMapper mapper)
        {
            _riotAPIService = riotAPIService;
            _mapper = mapper;
        }



        [HttpPut("UpdateRequest/")]
        public async Task<IActionResult> UpdateOngoingChallenge(Guid id )
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return BadRequest("API key is required");
            }

            var result = await _riotAPIService.UpdateOngoingChallenge(id, apiKey);

            if (result)
            {
                return Ok("Challenge updated successfully");
            }
            else
            {
                return BadRequest("Failed to update challenge");
            }
        }


        [HttpGet("GetSummoner")]
        public async Task<IActionResult> GetSummoner(string gameName, string tagLine)
        {
            try
            {
                
                var playerData = await _riotAPIService.GetSummonerAsync(gameName, tagLine, apiKey);

                return Ok(playerData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }
    }
}
