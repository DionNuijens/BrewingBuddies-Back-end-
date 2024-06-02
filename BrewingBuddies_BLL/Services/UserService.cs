using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;

namespace BrewingBuddies_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LeagueUserEntity> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            return user != null ? _mapper.Map<LeagueUserEntity>(user) : null;
        }

        public async Task<LeagueUserEntity> AddUserAsync(LeagueUserEntity user)
        {

            await _unitOfWork.Users.Create(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<bool> UpdateUserAsync(LeagueUserEntity user)
        {
            

            var existingUser = await _unitOfWork.Users.GetById(user.Id);

            if (existingUser == null)
                return false; 

            _mapper.Map(user, existingUser); 

            await _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return true; 
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllUsers()
        {
            var users = await _unitOfWork.Users.GetAll();

            return _mapper.Map<IEnumerable<LeagueUserEntity>>(users);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);

            if (user == null)
                return false;

            await _unitOfWork.Users.Delete(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
