using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys
{
    public class AccountDTO : BaseEntity
    {
        public string? puuid { get; set; }
        public string? gameName { get; set; }
        public string? tagLine { get; set; }
    }
}
