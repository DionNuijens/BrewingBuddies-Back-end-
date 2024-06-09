using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_Entitys.Dtos.Requests;
using BrewingBuddies_Entitys;
using Microsoft.AspNetCore.Mvc;

namespace BrewingBuddies.Controllers
{
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
            var createRequestDTO = await _requestService.AddUserAsync(requestEntity);

            // Check if the user was successfully added
            return true;
        }


    }
}
