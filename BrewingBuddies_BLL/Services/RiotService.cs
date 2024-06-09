using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Services
{
    public class RiotService : IRiotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RiotService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RiotEntity> AddUserAsync(RiotEntity user)
        {

            await _unitOfWork.RiotUsers.Create(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }
    }
}
