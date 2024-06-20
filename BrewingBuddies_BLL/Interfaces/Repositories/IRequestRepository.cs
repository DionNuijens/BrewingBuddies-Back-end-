using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Repositories
{
    public interface IRequestRepository : IGenericRepository<RequestEntity>
    {
        Task<IEnumerable<RequestEntity>> GetAllPending(Guid id);
        Task<IEnumerable<RequestEntity>> GetAllReceived(Guid id);
        Task<bool> Update(RequestEntity request);
        Task<IEnumerable<RequestEntity>> GetAllOngoing(Guid id);
        Task<IEnumerable<RequestEntity>> GetAllComplete(Guid id);
        Task<bool> DeleteRequest(Guid id); 




    }
}
