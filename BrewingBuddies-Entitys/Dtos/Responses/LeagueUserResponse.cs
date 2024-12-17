using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Responses
{
    public class LeagueUserResponse
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? RiotId { get; set; }

        public string? AccountId { get; set;}
    }
}
