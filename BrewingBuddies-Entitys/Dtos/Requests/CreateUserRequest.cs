using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class CreateUserRequest
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? UserName { get; set; }
    }
}
