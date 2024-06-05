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
    public class RegistrationRepository : GenericRepository<UserEntity>, IRegistrationRepository
    {
        public RegistrationRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        ////public virtual async Task<UserEntity> GetByNaam(string naam)
        ////{
        ////    return await _dbSet.FindAsync(naam);
        ////}

        //public async Task<UserEntity> SingleOrDefaultAsync(Func<UserEntity, bool> predicate)
        //{
        //    try
        //    {
        //        return await Task.FromResult(_dbSet.AsNoTracking().SingleOrDefault(predicate));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "{Repo} SingleOrDefaultAsync Function Error", typeof(RegistrationRepository));
        //        throw;
        //    }
        //}


        public async Task<UserEntity> GetByNaam(string naam)
        {
            return await _context.Set<UserEntity>().SingleOrDefaultAsync(u => u.naam == naam);
        }



    }
}
