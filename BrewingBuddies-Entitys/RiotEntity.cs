using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys
{
    public class RiotEntity 
    {
        public string? Id { get; set; }
        public string? summonerName { get; set; }
        public string? tagLine { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }

    }
}
