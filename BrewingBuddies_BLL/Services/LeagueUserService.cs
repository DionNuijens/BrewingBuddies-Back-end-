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
            if (userId== null)
            {
                throw new ArgumentException("User ID is not filled in.");
            }
            var user = await _unitOfWork.LeagueUsers.GetById(userId);
            return user != null ? _mapper.Map<LeagueUserEntity>(user) : null;
        }

        public async Task<LeagueUserEntity> AddUserAsync(LeagueUserEntity user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("User is not filled in.");
            }
            await _unitOfWork.LeagueUsers.Create(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<bool> UpdateUserAsync(LeagueUserEntity user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || user.Id == null)
            {
                throw new ArgumentException("User or User ID is not filled in."); 
            }

            var existingUser = await _unitOfWork.LeagueUsers.GetById(user.Id);

            if (existingUser == null)
                throw new InvalidOperationException("There is no user for this ID"); 

            _mapper.Map(user, existingUser); 

            await _unitOfWork.LeagueUsers.Update(user);
            await _unitOfWork.CompleteAsync();
            return true; 
        }



        public async Task<IEnumerable<LeagueUserEntity>> GetAllUsersFromAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("User ID is not filled in."); 
            }

            var users = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if(users.Count() == 0)
            {
                throw new InvalidCastException($"No users found for account with ID '{id}'.");

            }

            return _mapper.Map<IEnumerable<LeagueUserEntity>>(users);
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("User ID is not filled in."); 
            }

            var users = await _unitOfWork.LeagueUsers.GetAllFromNotAccount(id);

            var connectedUsers = new List<LeagueUserEntity>();

            foreach (var user in users)
            {
                if (user.RiotId != null)
                {
                    connectedUsers.Add(user);
                }

            }

            if (connectedUsers.Count() == 0)
            {
                throw new InvalidCastException($"No users found for account with ID '{id}'.");

            }

            return connectedUsers;
        }


        public async Task<bool> DeleteUser(Guid userId)
        {
            if (userId == null)
            {
                throw new ArgumentException("User ID is not filled in."); 
            }
            var user = await _unitOfWork.LeagueUsers.GetById(userId);

            if (user == null)
                throw new InvalidOperationException("There is no user for this ID");

            await _unitOfWork.LeagueUsers.Delete(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromAccountConnected(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("User ID is not filled in."); 
            }


            var users = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);

            if (users == null)
            {
                throw new InvalidCastException("Unable to get users"); 
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
