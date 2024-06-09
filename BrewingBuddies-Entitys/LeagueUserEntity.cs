using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys
{
    public class LeagueUserEntity : BaseEntity
    {
        public string? UserName { get; set; }
        public string? RiotId { get; set; }
        public Guid? AccountId { get; set; }

    }
}
