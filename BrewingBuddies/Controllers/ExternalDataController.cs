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
        private readonly string apiKey = "RGAPI-6463a897-6230-4300-9a04-cd59bb00c85c";
        public ValuesController(IRiotAPIService riotAPIService, IMapper mapper)
        {
            _riotAPIService = riotAPIService;
            _mapper = mapper;
        }



        [HttpPut("update/{id}")]
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

        //[HttpGet("Account")]
        //public async Task<IActionResult> GetPlayerData(string gameName, string tagLine)
        //{
        //    try
        //    {
        //        var playerData = await API_Request.GetAccountInfo(gameName, tagLine, apiKey);

        //        return Ok(playerData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

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
