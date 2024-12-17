using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class CreateRegistrationRequest
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? email { get; set; }
        public string? naam { get; set; }
        public string? hash { get; set; }

    }
}
