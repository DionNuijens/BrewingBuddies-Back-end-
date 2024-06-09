using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class CreateRiotEntityRequest
    {
        public string? Id { get; set; }
        public string? summonerName { get; set; }
        public string? tagLine { get; set; }

    }
}
