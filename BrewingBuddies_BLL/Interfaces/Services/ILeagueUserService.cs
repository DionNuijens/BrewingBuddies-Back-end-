using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Services
{
    public interface ILeagueUserService
    {
        Task<LeagueUserEntity> GetUserByIdAsync(Guid userId);
        Task<LeagueUserEntity> AddUserAsync(LeagueUserEntity user);
        Task<bool> UpdateUserAsync(LeagueUserEntity user);
        //Task<IEnumerable<LeagueUserEntity>> GetAllUsers();
        Task<bool> DeleteUser(Guid userId);
        Task<IEnumerable<LeagueUserEntity>> GetAllUsersFromAccount(string id);
        Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(string id);
        Task<IEnumerable<LeagueUserEntity>> GetAllFromAccountConnected(string id);



    }
}
