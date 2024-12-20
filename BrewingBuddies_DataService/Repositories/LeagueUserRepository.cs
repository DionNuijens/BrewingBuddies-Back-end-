﻿using BrewingBuddies_DataService.Data;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_DataService.Repositories
{
    public class LeagueUserRepository : GenericRepository<LeagueUserEntity>, ILeagueUserRepository
    {
        public LeagueUserRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromAccount(string accountId)
        {
            try
            {
                return await _context.LeagueUsers
                    .Where(x => x.Status == 1 && x.AccountId == accountId)  
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "{Repo} All function error", typeof(LeagueUserRepository));
                throw;
            }
        }

        public async Task<IEnumerable<LeagueUserEntity>> GetAllFromNotAccount(string id)
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1 && x.AccountId != id)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();

            }
            catch (Exception e)
            {

                _logger.LogError(e, message: "{Repo} All function error", typeof(LeagueUserRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result  = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdateDate = DateTime.UtcNow;

                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "{Repo} Delete function error", typeof(LeagueUserRepository));
                throw;
            }
        }

        public override async Task<bool> Update(LeagueUserEntity userDTO)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == userDTO.Id);

                if (result == null)
                    return false;

                result.UpdateDate = DateTime.UtcNow;
                result.UserName = userDTO.UserName;
                result.RiotId = userDTO.RiotId;

                return true; 

            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "{Repo} Update function error", typeof(LeagueUserRepository));
                throw;
            }
        }
    }
}
