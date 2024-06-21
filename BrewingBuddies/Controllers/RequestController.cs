using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_Entitys.Dtos.Requests;
using BrewingBuddies_Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IMapper _mapper;
        private IRequestService _requestService;

        public RequestController(IMapper mapper, IRequestService requestService)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        [HttpPost("AddRequest")]
        public async Task<bool> AddUser([FromBody] CreateRequestRequest request)
        {
            if (!ModelState.IsValid)
                return false;

            var requestEntity = _mapper.Map<RequestEntity>(request);
            var createRequestDTO = await _requestService.AddRequest(requestEntity);

            return true;
        }

        [HttpGet]
        [Route("challenger")]
        public async Task<IActionResult> GetAllRequests(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _requestService.GetAllPending(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("defender")]
        public async Task<IActionResult> GetAllRequestss(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _requestService.GetAllReceived(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("ongoing")]
        public async Task<IActionResult> GetAllRequestsss(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _requestService.GetAllOngoing(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("complete")]
        public async Task<IActionResult> GetAllComplete(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _requestService.GetAllComplete(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPut("updateRequest")]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateRequestRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userDto = _mapper.Map<RequestEntity>(user);
            var updateResult = await _requestService.UpdateRequestAsync(userDto);

            if (!updateResult)
                return NotFound("User not found");

            return NoContent();
        }

        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _requestService.DeleteRuest(userId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return StatusCode(200);
        }


    }
}
