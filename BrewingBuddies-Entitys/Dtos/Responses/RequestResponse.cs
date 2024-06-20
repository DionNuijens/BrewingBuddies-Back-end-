using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Responses
{
    public class RequestResponse
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public Guid challenger { get; set; }
        public Guid defender { get; set; }
        public decimal challengerKDA { get; set; }
        public decimal defenderKDA { get; set; }
        public string? winner { get; set; }
    }
}
