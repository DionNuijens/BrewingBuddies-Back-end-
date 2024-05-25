using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(Guid userId);
        Task<UserDTO> AddUserAsync(UserDTO user);
        Task<bool> UpdateUserAsync(UserDTO user);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<bool> DeleteUser(Guid userId);




    }
}
