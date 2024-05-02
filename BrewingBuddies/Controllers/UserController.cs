using AutoMapper;
using BrewingBuddies_DataService.Repositories.Interfaces;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BrewingBuddies.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var users = await _unitOfWork.User.GetById(userId);

            if (users == null)
                return NotFound("Users not found");

            var result = _mapper.Map<UserDTO>(users);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _mapper.Map<UserDTO>(user);

            await _unitOfWork.User.Create(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetUser), new { userId = result.Id }, result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _mapper.Map<UserDTO>(user);

            await _unitOfWork.User.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAll();

            var result = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{UserId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _unitOfWork.User.GetById(userId);

            if (user == null)
                return NotFound();

            await _unitOfWork.User.Delete(userId);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
