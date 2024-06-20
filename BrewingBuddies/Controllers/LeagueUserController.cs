using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies.Controllers
{
    // At more checks en return status codes (check the delete function)
    //[Authorize]
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
        [Route("account")]
        public async Task<IActionResult> GetAllUsersFromAccoutn(string AccountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _userService.GetAllUsersFromAccount(AccountId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var userDto = await _userService.GetUserByIdAsync(userId);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] CreateLeagueUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userDto = _mapper.Map<LeagueUserEntity>(user);
            var createdUserDto = await _userService.AddUserAsync(userDto);

            return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateLeagueUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userDto = _mapper.Map<LeagueUserEntity>(user);
            var updateResult = await _userService.UpdateUserAsync(userDto);

            if (!updateResult)
                return NotFound("User not found");

            return NoContent();
        }

        //[HttpGet("GetUsers")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var users = await _userService.GetAllUsers();

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(users);
        //}

        [HttpGet]
        [Route("ConnectedAccounts")]
        public async Task<IActionResult> GetAllUsersConnected(string AccountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _userService.GetAllFromAccountConnected(AccountId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("NotAccount")]
        public async Task<IActionResult> GetAllUsersFromNotAccoutn(string AccountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = await _userService.GetAllFromNotAccount(AccountId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _userService.DeleteUser(userId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return StatusCode(200);
        }
    }
}
