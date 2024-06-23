using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BrewingBuddies_DataService.Data;
using BrewingBuddies_DataService.Repositories;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace IntergrationTests
{
    public class LeagueUserRepositoryTest : IDisposable
    {
        private AppDbContext _context;
        private LeagueUserRepository _userRepository;
        private Mock<ILogger<LeagueUserRepository>> _mockLogger;

        public LeagueUserRepositoryTest() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            Data();

            _mockLogger = new Mock<ILogger<LeagueUserRepository>>();
            _userRepository = new LeagueUserRepository(_context, _mockLogger.Object);
        }

        private void Data()
        {
            LeagueUserEntity defaultUser = new LeagueUserEntity
            {
                Id = new Guid("01358d6f-0eb6-465d-8f41-480510de6302"),
                UserName = "User1",
                AccountId = "auth0-123",
                RiotId = "RiotId",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };
            LeagueUserEntity defaultUser2 = new LeagueUserEntity
            {
                Id = new Guid("141ff716-20ba-4552-a32f-f4a41059b034"),
                UserName = "User2",
                AccountId = "auth0-123",
                RiotId = "RiotId",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            _context.LeagueUsers.Add(defaultUser);
            _context.LeagueUsers.Add(defaultUser2);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }

        [Fact]
        public async Task GetAllUsersFromAccountAsync_ReturnsAllUsers()
        {
            string AccountID = "auth0-123";
            // Act
            var result = await _userRepository.GetAllFromAccount(AccountID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.UserName == "User1");
            Assert.Contains(result, u => u.UserName == "User2");

        }

        [Fact]
        public async Task GetUserById_ReturnsExpectedUser()
        {
            Guid userID = new Guid("141ff716-20ba-4552-a32f-f4a41059b034");

            LeagueUserEntity result = await _userRepository.GetById(userID);

            Assert.NotNull(result);
            Assert.Equal(result.Id, userID);
        }

        [Fact]
        public async Task AddUser_SavesUserToDatabase()
        {
            LeagueUserEntity NewUser = new()
            {
                Id = new Guid("1b6b5104-e24f-4a57-933d-737bc685832d"),
                UserName = "NewUser",
                AccountId = "auth0-123",
                RiotId = "RiotId",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            // Act
            _userRepository?.Create(NewUser);
            // Assert
            var User = await _userRepository.GetById(NewUser.Id);
            Assert.NotNull(User);
            Assert.Equal(NewUser.UserName, User.UserName);
        }

        [Fact]
        public async Task UpdateUser_SaveUserToDatabase()
        {
            Guid userID = new Guid("141ff716-20ba-4552-a32f-f4a41059b034");

            LeagueUserEntity User = await _userRepository.GetById(userID);
            User.UserName = "UpdatedUser";
            await _userRepository.Update(User);
            LeagueUserEntity ExpectedUser = await _userRepository.GetById(userID);

            Assert.Equal(ExpectedUser.UserName, User.UserName);
        }

        public async Task UpdateUser_InvalidUserId_ReturnsFalse()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid(); 
            var user = new LeagueUserEntity
            {
                Id = invalidUserId,
                UserName = "UpdatedUser"
            };

            // Act
            var result = await _userRepository.Update(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUser_DeletesUserFromDatabase()
        {
            Guid userToDelete = new Guid("141ff716-20ba-4552-a32f-f4a41059b034");

            await _userRepository.Delete(userToDelete);
            var deletedUser = await _context.LeagueUsers.FindAsync(userToDelete);

            Assert.Equal(deletedUser?.Status, 0);
        }

        [Fact]
        public async Task DeleteUser_InvalidUserId_ReturnsFalse()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid(); 

            // Act
            var result = await _userRepository.Delete(invalidUserId);

            // Assert
            Assert.False(result);
        }





    }
}