using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Services
{
    public interface IRequestService
    {
        Task<RequestEntity> AddUserAsync(RequestEntity request);
    }
}
