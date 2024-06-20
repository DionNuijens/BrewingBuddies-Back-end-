using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Repositories
{
    public interface IRiotAPIRepository
    {
        Task<List<string>> GetMatchIDs(string api_key, string puuid);
        Task<DateTime?> GetMatchStartTimeAsync(string apiKey, string matchId);
        Task<decimal?> GetKdaAsync(string apiKey, string matchId, string summonerId);
    }
}
