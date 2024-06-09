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
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RequestEntity> AddUserAsync(RequestEntity request)
        {

            await _unitOfWork.Requests.Create(request);
            await _unitOfWork.CompleteAsync();

            return request;
        }



    }
}
