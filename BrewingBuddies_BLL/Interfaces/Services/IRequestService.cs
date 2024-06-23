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
        Task<RequestEntity> AddRequest(RequestEntity request);
        Task<IEnumerable<RequestObject>> GetAllPending(string id);
        Task<IEnumerable<RequestObject>> GetAllReceived(string id);
        Task<bool> UpdateRequestAsync(RequestEntity request);
        Task<IEnumerable<RequestObject>> GetAllOngoing(string id);
        Task<IEnumerable<RequestObject>> GetAllComplete(string id);
        Task<bool> DeleteRuest(Guid userId);


    }
}
