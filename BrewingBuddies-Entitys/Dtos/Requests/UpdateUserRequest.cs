using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class UpdateUserRequest
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
    }
}
