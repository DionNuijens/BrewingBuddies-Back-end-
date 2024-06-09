﻿using BrewingBuddies_Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Interfaces.Repositories
{
    public interface ILeagueUserRepository : IGenericRepository<LeagueUserEntity>
    {
        Task<IEnumerable<LeagueUserEntity>> GetAllFromAccount(Guid id);
        Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(Guid id);
    }
}
