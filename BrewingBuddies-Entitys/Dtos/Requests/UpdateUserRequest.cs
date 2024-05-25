using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
    }
}
