using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys.Dtos.Requests;

namespace BrewingBuddies_BLL.Services
{
    public class LeagueUserService : ILeagueUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeagueUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LeagueUserEntity> GetUserByIdAsync(Guid userId)
        {
            if (userId== Guid.Empty)
            {
                throw new ArgumentNullException("User ID is not filled in.");
            }
            var user = await _unitOfWork.LeagueUsers.GetById(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Unable to retrieve user.");
            }

            return _mapper.Map<LeagueUserEntity>(user);
        }

        public async Task<LeagueUserEntity> AddUserAsync(CreateLeagueUserRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentNullException("Username is not filled in.");
            }
            var userDTO = _mapper.Map<LeagueUserEntity>(user);
            await _unitOfWork.LeagueUsers.Create(userDTO);
            await _unitOfWork.CompleteAsync();

            return userDTO;
        }

        public async Task<bool> UpdateUserAsync(LeagueUserEntity user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || user.Id == Guid.Empty)
            {
                throw new ArgumentNullException("Username or User ID is not filled in."); 
            }

            var existingUser = await _unitOfWork.LeagueUsers.GetById(user.Id);

            if (existingUser == null)
            {
                throw new InvalidOperationException("Unable to retrieve user.");
            }

            //_mapper.Map(user, existingUser); 

            await _unitOfWork.LeagueUsers.Update(user);
            await _unitOfWork.CompleteAsync();
            return true; 
        }



        public async Task<IEnumerable<LeagueUserEntity>> GetAllUsersFromAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }

            var users = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if (users == null)
            {
                throw new InvalidOperationException("Unable to retrieve users from account.");
            }

            return _mapper.Map<IEnumerable<LeagueUserEntity>>(users);
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }

            var users = await _unitOfWork.LeagueUsers.GetAllFromNotAccount(id);

            if (users == null)
            {
                throw new InvalidOperationException("Unable to retrieve users from accounts.");
            }
            var connectedUsers = new List<LeagueUserEntity>();

            foreach (var user in users)
            {
                if (user.RiotId != null)
                {
                    connectedUsers.Add(user);
                }

            }

            return connectedUsers;
        }


        public async Task<bool> DeleteUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User ID is not filled in."); 
            }
            var user = await _unitOfWork.LeagueUsers.GetById(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Unable to find user from account.");
            }

            await _unitOfWork.LeagueUsers.Delete(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromAccountConnected(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }


            var users = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);

            if (users == null)
            {
                throw new InvalidOperationException("Unable to retrieve users from account.");
            }

            var connectedUsers = new List<LeagueUserEntity>();

            foreach (var user in users)
            {
                if (!string.IsNullOrWhiteSpace(user.RiotId))
                {
                    connectedUsers.Add(user); 
                }
            }

            return connectedUsers;
        }

    }
}
