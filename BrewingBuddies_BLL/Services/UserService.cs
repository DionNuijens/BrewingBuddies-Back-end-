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

        public async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.User.GetById(userId);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO user)
        {

            await _unitOfWork.User.Create(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            

            var existingUser = await _unitOfWork.User.GetById(user.Id);

            if (existingUser == null)
                return false; 

            _mapper.Map(user, existingUser); 

            await _unitOfWork.User.Update(user);
            await _unitOfWork.CompleteAsync();
            return true; 
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAll();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _unitOfWork.User.GetById(userId);

            if (user == null)
                return false;

            await _unitOfWork.User.Delete(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
