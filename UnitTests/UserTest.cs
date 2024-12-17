using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using BrewingBuddies_Entitys;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_BLL.Interfaces;
using BrewingBuddies_BLL.Interfaces.Repositories;
using AutoMapper;
using BrewingBuddies_Entitys.Dtos.Requests;

namespace UnitTests
{
    public class UserTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILeagueUserRepository> _mockUserRepository;

        public UserTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockUserRepository = new Mock<ILeagueUserRepository>();
        }

        [Fact]
        public async Task GetUserByIdAsync_WithValidUserId_ReturnsUserDTO()
        {
            // Arrange
            Guid validUserId = Guid.NewGuid();
            LeagueUserEntity validUser = new LeagueUserEntity
            {
                Id = validUserId,
                UserName = "Test"
            };
            LeagueUserEntity expectedUserDTO = new LeagueUserEntity
            {
                Id = validUserId,
                UserName = "Test"
            };

            _mockUserRepository.Setup(repo => repo.GetById(validUserId)).ReturnsAsync(validUser);
            _mockMapper.Setup(mapper => mapper.Map<LeagueUserEntity>(validUser)).Returns(expectedUserDTO);

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            LeagueUserEntity actualUser = await userService.GetUserByIdAsync(validUserId);

            // Assert
            Assert.Equal(expectedUserDTO.Id, actualUser.Id);
            Assert.Equal(expectedUserDTO.UserName, actualUser.UserName);
        }

        [Fact]
        public async Task AddUserAsync_AddsUserAndReturnsTrue()
        {
            // Arrange
            LeagueUserEntity newUser = new LeagueUserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "NewUser"
            };
            CreateLeagueUserRequest user = new CreateLeagueUserRequest
            {               
                UserName = "NewUser"
            };
            _mockMapper.Setup(mapper => mapper.Map<LeagueUserEntity>(user)).Returns(newUser);
            _mockUserRepository.Setup(repo => repo.Create(newUser)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            LeagueUserEntity createdUser = await userService.AddUserAsync(user);

            // Assert
            Assert.Equal(newUser.Id, createdUser.Id);
            Assert.Equal(newUser.UserName, createdUser.UserName);
        }

        [Fact]
        public async Task UpdateUserAsync_WithValidUser_ReturnsTrue()
        {
            // Arrange
            LeagueUserEntity existingUser = new LeagueUserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "ExistingUser"
            };

            LeagueUserEntity updatedUser = new LeagueUserEntity
            {
                Id = existingUser.Id,
                UserName = "UpdatedUser"
            };

            
            _mockUserRepository.Setup(repo => repo.GetById(existingUser.Id)).ReturnsAsync(existingUser);
            _mockMapper.Setup(mapper => mapper.Map(updatedUser, existingUser)).Returns(existingUser);
            _mockUserRepository.Setup(repo => repo.Update(existingUser)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            bool result = await userService.UpdateUserAsync(updatedUser);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserAsync_WithInvalidUser_ThrowsArgumentException()
        {
            // Arrange
            LeagueUserEntity invalidUser = new LeagueUserEntity
            {
                Id = Guid.NewGuid(), 
                UserName = ""  
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var userService = new LeagueUserService(mockUnitOfWork.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userService.UpdateUserAsync(invalidUser);
            });
        }

        [Fact]
        public async Task GetAllUsersFromAccount_ReturnsUsers()
        {
            // Arrange
            string accountId = "accountId";
            IEnumerable<LeagueUserEntity> users = new List<LeagueUserEntity>
            {
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "User1" },
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "User2" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllFromAccount(accountId)).ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<LeagueUserEntity>>(users)).Returns(users);

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            IEnumerable<LeagueUserEntity> result = await userService.GetAllUsersFromAccount(accountId);

            // Assert
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task DeleteUser_WithValidUserId_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            LeagueUserEntity user = new LeagueUserEntity { Id = userId, UserName = "User" };

            _mockUserRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.Delete(userId)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            bool result = await userService.DeleteUser(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_WithInvalidUserId_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _mockUserRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync((LeagueUserEntity)null);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act and Assert
            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await userService.DeleteUser(userId));

            // Assert
            Assert.NotNull(ex);
            Assert.Equal("Unable to find user from account.", ex.Message);
        }

        [Fact]
        public async Task GetAllFromAccountConnected_ReturnsConnectedUsers()
        {
            // Arrange
            string accountId = "accountId";
            IEnumerable<LeagueUserEntity> users = new List<LeagueUserEntity>
            {
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "User1", RiotId = "Riot1" },
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "User2", RiotId = null },
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "User3", RiotId = "Riot3" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllFromAccount(accountId)).ReturnsAsync(users);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);


            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            IEnumerable<LeagueUserEntity> result = await userService.GetAllFromAccountConnected(accountId);

            Assert.NotNull(result);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, user => Assert.NotNull(user.RiotId));
        }

    }

}

