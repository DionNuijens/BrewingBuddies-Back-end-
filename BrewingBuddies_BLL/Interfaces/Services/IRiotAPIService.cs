using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Services
{
    public interface IRiotAPIService
    {
        Task<bool> UpdateOngoingChallenge(Guid id, string api_key);
    }
}
