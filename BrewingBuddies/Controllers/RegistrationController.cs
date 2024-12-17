using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys.Dtos.Requests;
using BrewingBuddies_Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly IMapper _mapper;
        private IRegistrationService _registrationService;

        public RegistrationController(IMapper mapper, IRegistrationService registrationService)
        {
            _mapper = mapper;
            _registrationService = registrationService;
        }

        //[HttpPost("AddUserrr")]
        //public async Task<IActionResult> AddUser([FromBody] CreateRegistrationRequest user)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var userEntity = _mapper.Map<UserEntity>(user);
        //    var createdUserDto = await _registrationService.AddUserAsync(userEntity);

        //    return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
        //}


        //[HttpGet]
        //[Route("hihihi{userId:guid}")]
        //public async Task<IActionResult> GetUser(Guid userId)
        //{
        //    var userDto = await _registrationService.GetUserByIdAsync(userId);

        //    if (userDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userDto);
        //}

        //[HttpGet("{userName}")]
        //public async Task<IActionResult> GetUserByName(string userName)
        //{
        //    var userDto = await _registrationService.GetUserByNaamAsync(userName);

        //    if (userDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userDto);
        //}

        //[HttpGet("Inlog")]
        //public async Task<IActionResult> Inlog(string userName, string hash)
        //{
        //    var userEntity=  await _registrationService.LoginAsync(userName, hash);

        //    if (userEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userEntity);
        //}
    }
}
