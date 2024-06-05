using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BrewingBuddies_BLL.Interfaces.Services;

namespace BrewingBuddies.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILeagueUserService _service;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, ILeagueUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = userService;
        }
    }
}
