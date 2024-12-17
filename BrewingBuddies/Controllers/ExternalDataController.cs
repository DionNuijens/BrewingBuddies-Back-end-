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
        //in appestting.jason
        private readonly string apiKey = "RGAPI-46638c7d-d61b-45c4-a7aa-2b2c4edcb73f";
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
