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
        Task<LeagueUserEntity> GetUserByIdAsync(Guid userId);
        Task<LeagueUserEntity> AddUserAsync(LeagueUserEntity user);
        Task<bool> UpdateUserAsync(LeagueUserEntity user);
        Task<IEnumerable<LeagueUserEntity>> GetAllUsers();
        Task<bool> DeleteUser(Guid userId);




    }
}
