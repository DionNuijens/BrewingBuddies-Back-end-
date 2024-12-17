using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ILeagueUserRepository LeagueUsers { get; }
        IRegistrationRepository Registration { get; }
        IRiotRepository RiotUsers { get; } 
        IRequestRepository Requests { get; }

        Task<bool> CompleteAsync();
    }
}
