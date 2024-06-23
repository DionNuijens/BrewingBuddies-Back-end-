using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies.Controllers
{
    [Authorize]
    public class LeagueUserController : ControllerBase 
    {
        private ILeagueUserService _userService;
        private IMapper _mapper;
        public LeagueUserController(ILeagueUserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAccounts")]
        public async Task<IActionResult> GetAllUsersFromAccoutn(string AccountId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var users = await _userService.GetAllUsersFromAccount(AccountId);

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
                
               Console.WriteLine($"Unexpected error while retrieving users for account ID '{AccountId}'");
                return StatusCode(500, "Internal server error"); 
            }

        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            try
            {
                var userDto = await _userService.GetUserByIdAsync(userId);

                if (userDto == null)
                {
                    return NotFound(); 
                }

                return Ok(userDto); 
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while retrieving user with ID '{userId}'");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] CreateLeagueUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var userDto = _mapper.Map<LeagueUserEntity>(user);
                var createdUserDto = await _userService.AddUserAsync(userDto);

                return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid user data provided.");
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while adding user.");
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateLeagueUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var userDto = _mapper.Map<LeagueUserEntity>(user);
                var updateResult = await _userService.UpdateUserAsync(userDto);

                if (!updateResult)
                {
                    return NotFound("User not found"); 
                }

                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid user data provided.");
                return BadRequest(ex.Message); 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("User not found for update.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while updating user.");
                return StatusCode(500, "Internal server error"); 
            }
        }


        [HttpGet]
        [Route("GetConnectedAccounts")]
        public async Task<IActionResult> GetAllUsersConnected(string AccountId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var users = await _userService.GetAllFromAccountConnected(AccountId);

                if (users == null || users.Count()== 0)
                {
                    return NotFound("No connected users found for the provided account ID."); 
                }

                return Ok(users); 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No users found for the provided account ID.");
                return NotFound(ex.Message); 
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while retrieving connected users.");
                return StatusCode(500, "Internal server error"); 
            }

        }

        [HttpGet]
        [Route("GetNotAccount")]
        public async Task<IActionResult> GetAllUsersFromNotAccoutn(string AccountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var users = await _userService.GetAllFromNotAccount(AccountId);

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
                Console.WriteLine("User not found for deletion.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Unexpected error while retrieving users for account ID '{AccountId}'");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID."); 
            }

            try
            {
                await _userService.DeleteUser(userId);
                return Ok(); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid user ID provided.");
                return BadRequest(ex.Message); 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("User not found for deletion.");
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred while deleting user.");
                return StatusCode(500, "Internal server error"); 
            }
        }
    }
}
