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
        public async Task<IActionResult> AddRequest([FromBody] CreateRequestRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var requestEntity = _mapper.Map<RequestEntity>(request);
                await _requestService.AddRequest(requestEntity);

                return Ok(true);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid request data provided.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetPending")]
        public async Task<IActionResult> GetAllRequests(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
            var users = await _requestService.GetAllPending(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);

            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpGet]
        [Route("GetReceived")]
        public async Task<IActionResult> GetAllRequestss(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
            var users = await _requestService.GetAllReceived(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);

            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
        }

        [HttpGet]
        [Route("GetOngoing")]
        public async Task<IActionResult> GetAllRequestsss(string AccountID)
        {
            try
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
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpGet]
        [Route("GetComplete")]
        public async Task<IActionResult> GetAllComplete(string AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
            var users = await _requestService.GetAllComplete(AccountID);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);

            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPut("updateRequest")]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateRequestRequest user)
        {
            try
            {
            if (!ModelState.IsValid)
                return BadRequest();

            var userDto = _mapper.Map<RequestEntity>(user);
            var updateResult = await _requestService.UpdateRequestAsync(userDto);

            if (!updateResult)
                return NotFound("User not found");

            return NoContent();

            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error"); 
            }
        }


        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _requestService.DeleteRuest(userId);
                return StatusCode(200);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding request.");
                return StatusCode(500, "Internal server error");
            }

        }


    }
}
