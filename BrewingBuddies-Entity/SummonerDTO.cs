using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrewingBuddies_Entity
{
    public class SummonerDTO
    {
        public string accountId {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }

    }
}
