using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Services
{
    public interface IRegistrationService
    {
        Task<UserEntity> AddUserAsync(UserEntity user);
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        ////Task<UserEntity> GetUserByNameAsync(string naam);
        Task<UserEntity> GetUserByNaamAsync(string userName);
        Task<UserEntity> LoginAsync(string userName, string hash);
    }
}
