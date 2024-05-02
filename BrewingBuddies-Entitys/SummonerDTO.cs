using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys
{
    public class SummonerDTO : BaseEntity
    {
        public string? accountId { get; set; }
        public int profileIconId { get; set; }
        public long revisionDate { get; set; }
        public string? id { get; set; }
        public string? puuid { get; set; }
        public long summonerLevel { get; set; }
        public string? summonerName { get; set; }

    }
}
