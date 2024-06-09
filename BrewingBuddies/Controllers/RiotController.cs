using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_Entitys.Dtos.Requests;
using BrewingBuddies_Entitys;
using Microsoft.AspNetCore.Mvc;

namespace BrewingBuddies.Controllers
{
    public class RiotController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRiotService _riotRepository;

        public RiotController (IMapper mapper, IRiotService riotRepository)
        {
            _mapper = mapper;
            _riotRepository = riotRepository;
        }

        [HttpPost("LinkAccount")]
        public async Task<bool> AddLink([FromBody] CreateRiotEntityRequest user)
        {
            if (!ModelState.IsValid)
                return false;

            var userEntity = _mapper.Map<RiotEntity>(user);
            var createdUserDto = await _riotRepository.AddUserAsync(userEntity);

            // Check if the user was successfully added
            return true;
        }
    }
}
