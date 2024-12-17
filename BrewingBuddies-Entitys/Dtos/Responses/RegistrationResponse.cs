using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Responses
{
    public class RegistrationResponse
    {
        public Guid UserId { get; set; } 
        public string? email { get; set; }
        public string? naam { get; set; } 
        public string? hash { get; set; }
    }
}
