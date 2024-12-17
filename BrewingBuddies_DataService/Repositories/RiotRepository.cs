using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_DataService.Data;
using BrewingBuddies_Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_DataService.Repositories
{
    public class RiotRepository : GenericRepository<RiotEntity>, IRiotRepository
    {
        public RiotRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
