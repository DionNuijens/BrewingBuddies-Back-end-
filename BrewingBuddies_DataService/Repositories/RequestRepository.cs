using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_DataService.Data;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_DataService.Repositories
{
    public class RequestRepository : GenericRepository<RequestEntity>, IRequestRepository
    {
        public RequestRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<RequestEntity>> GetAllPending(Guid id)
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1 && x.State == 0 && x.challenger == id && x.defender != id)
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

        public async Task<IEnumerable<RequestEntity>> GetAllReceived(Guid id)
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1 && x.State == 0 && x.challenger != id && x.defender == id)
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

        public async Task<IEnumerable<RequestEntity>> GetAllOngoing(Guid id)
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1 && x.State == 1 && (x.challenger == id || x.defender == id))
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

        public async Task<IEnumerable<RequestEntity>> GetAllComplete(Guid id)
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1 && x.State == 2 && (x.challenger == id || x.defender == id))
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

        public override async Task<bool> Update(RequestEntity request)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (result == null)
                    return false;

                result.UpdateDate = DateTime.UtcNow;
                result.State++;

                result.challengerKDA = request.challengerKDA;
                result.defenderKDA = request.defenderKDA;
                result.winner = request.winner;
                


                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "{Repo} Update function error", typeof(LeagueUserRepository));
                throw;
            }
        }
        public async Task<bool> DeleteRequest(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdateDate = DateTime.UtcNow;

                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "{Repo} Delete function error", typeof(RequestEntity));
                throw;
            }
        }
    }
}
