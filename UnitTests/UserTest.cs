using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BrewingBuddies_DataService.Repositories;
using BrewingBuddies_Entitys;
using BrewingBuddies_DataService.Data;
using BrewingBuddies_DataService.Repositories.Interfaces;
using FluentAssertions;


namespace UnitTests
{
    public class UserTest
    {
        //private readonly Mock<IUserContext> _context;
        //private readonly UserRepository _repository;
        //private readonly ILogger _logger;
        //public readonly ILogger _logger;



        [Fact]
        public void Test()
        {
            int a = 1;
            int b = 2;
            int c = a + b;


            Assert.Equal((a+b), c);
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
}
