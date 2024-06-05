using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using BrewingBuddies_Entitys;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_BLL.Interfaces;
using BrewingBuddies_BLL.Interfaces.Repositories;
using AutoMapper;

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

        //[Fact]
        //public void Test()
        //{
        //    int a = 1;
        //    int b = 2;
        //    int c = a + b;

        //    Assert.Equal((a + b), c);
        //}

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

            // Here you were using uninitialized mocks, let's initialize them first.
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers).Returns(_mockUserRepository.Object);

            var userService = new LeagueUserService(_mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            LeagueUserEntity actualUser = await userService.GetUserByIdAsync(validUserId);

            // Assert
            Assert.Equal(expectedUserDTO.Id, actualUser.Id);
            Assert.Equal(expectedUserDTO.UserName, actualUser.UserName);
        }
    }

    //[Fact]
    //public async Task AddUser_WhenCalled_ReturnsUsers()
    //{
    //    // Arrange
    //    Guid user1Id = Guid.NewGuid();
    //    Guid user2Id = Guid.NewGuid();
    //    Guid user3Id = Guid.NewGuid();
    //    var users = new List<UserDTO>
    //    {
    //        new UserDTO { Id = user1Id, UserName = "User1" ,Status = 1, AddedDate = DateTime.Now.AddDays(-3) },
    //    };
    //    var data = users.AsQueryable();
    //    var mockdbContext = new Mock<AppDbContext>();
    //    var mockSet = new Mock<DbSet<UserDTO>>();
    //    //var mockLogger = new Mock<ILogger>();
    //    var mockLogger = _logger;
    //    mockSet.As<IQueryable<UserDTO>>().Setup(x => x.Provider).Returns(data.Provider);
    //    mockSet.As<IQueryable<UserDTO>>().Setup(x => x.ElementType).Returns(data.ElementType);
    //    mockSet.As<IQueryable<UserDTO>>().Setup(x => x.Expression).Returns(data.Expression);
    //    mockSet.As<IQueryable<UserDTO>>().Setup(x => x.GetEnumerator()).Returns(() => data.GetEnumerator());
    //    mockdbContext.Setup(x => x.Users).Returns(mockSet.Object);

    //    mockdbContext.Setup(x => x.SaveChanges()).Returns(1);

    //    IUserRepository repository = new UserRepository(mockdbContext.Object, mockLogger);

    //    // Act
    //    //var response = await repository.Create(users.FirstOrDefault());
    //    var result = await repository.Create(users.FirstOrDefault());

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(1, 1);
    //}




}

