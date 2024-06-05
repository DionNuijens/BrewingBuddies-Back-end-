using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class CreateLeagueUserRequest
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? UserName { get; set; }
        public Guid? AccountId { get; set; }

    }
}
