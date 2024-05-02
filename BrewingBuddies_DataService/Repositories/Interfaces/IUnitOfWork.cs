using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        Task<bool> CompleteAsync();
    }
}
