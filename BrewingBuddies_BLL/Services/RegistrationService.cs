using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegistrationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserEntity> AddUserAsync(UserEntity user)
        {

            await _unitOfWork.Registration.Create(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.Registration.GetById(userId);
            return user != null ? _mapper.Map<UserEntity>(user) : null;
        }

        public async Task<UserEntity> GetUserByNaamAsync(string userName)
        {
            return await _unitOfWork.Registration.GetByNaam(userName);
        }

        public async Task<UserEntity> LoginAsync(string userName, string hash)
        {
            var givenUser = await GetUserByNaamAsync(userName);

            if (givenUser == null)
            {
                return null;
            }
            if(givenUser.naam != userName || givenUser.hash != hash)
            {
                return null;
            }

            return givenUser;
        }

        //public async Task<UserEntity> GetUserByNameAsync(string userName)
        //{
        //    var user = await _unitOfWork.Registration.GetByNaamm(userName);
        //    return user != null ? _mapper.Map<UserEntity>(user) : null;
        //}
    }
}
