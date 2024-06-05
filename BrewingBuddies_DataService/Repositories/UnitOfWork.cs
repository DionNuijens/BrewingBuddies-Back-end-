using BrewingBuddies_DataService.Data;
using BrewingBuddies_BLL.Interfaces.Repositories;
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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public ILeagueUserRepository LeagueUsers { get; }
        public IRegistrationRepository Registration { get; }
        

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            LeagueUsers = new LeagueUserRepository(context, logger);
            Registration = new RegistrationRepository(context, logger);
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
