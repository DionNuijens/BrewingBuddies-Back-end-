using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;
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
        Task<LeagueUserEntity> AddUserAsync(CreateLeagueUserRequest user);
        Task<bool> UpdateUserAsync(LeagueUserEntity user);
        Task<bool> DeleteUser(Guid userId);
        Task<IEnumerable<LeagueUserEntity>> GetAllUsersFromAccount(string id);
        Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(string id);
        Task<IEnumerable<LeagueUserEntity>> GetAllFromAccountConnected(string id);



    }
}
