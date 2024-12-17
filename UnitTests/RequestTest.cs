using AutoMapper;
using BrewingBuddies_BLL.Hubs;
using BrewingBuddies_BLL.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_Entitys;
using Xunit;

namespace UnitTests
{
    public class RequestTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRequestRepository> _mockRequestRepository;
        private readonly Mock<IHubContext<NotificationHub>> _mockHubContext;
        private readonly Mock<ILeagueUserRepository> _mockLeagueUserRepository;
        private readonly Mock<IHubClients> _mockHubClients;
        private readonly Mock<IClientProxy> _mockClientProxy;


        public RequestTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockRequestRepository = new Mock<IRequestRepository>();
            _mockHubContext = new Mock<IHubContext<NotificationHub>>();
            _mockLeagueUserRepository = new Mock<ILeagueUserRepository>();
            _mockHubClients = new Mock<IHubClients>();
            _mockClientProxy = new Mock<IClientProxy>();

            _mockHubContext.Setup(x => x.Clients).Returns(_mockHubClients.Object);
            _mockHubContext.Setup(hub => hub.Clients).Returns(_mockHubClients.Object);

        }

        [Fact]
        public async Task DeleteRequest_RequestExists_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fakeRequest = new RequestEntity
            {
                Id = userId,
                challenger = Guid.NewGuid(),
                defender = Guid.NewGuid(),
                challengerKDA = 3.5m,
                defenderKDA = 2.5m,
                winner = "",
                State = 0
            };

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(userId)).ReturnsAsync(fakeRequest);

            _mockUnitOfWork.Setup(uow => uow.Requests.DeleteRequest(userId)).ReturnsAsync(true);

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            var requestService = new RequestService(_mockUnitOfWork.Object,_mockMapper.Object,_mockHubContext.Object);

            // Act
            var result = await requestService.DeleteRuest(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteRequest_RequestDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(userId))
                           .ReturnsAsync((RequestEntity)null);

            var requestService = new RequestService(_mockUnitOfWork.Object, _mockMapper.Object, _mockHubContext.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await requestService.DeleteRuest(userId));
            Assert.Equal("Unable To retrieve Request", exception.Message);
        }

        [Fact]
        public async Task AddRequest_WithValidRequest_ReturnsRequestEntity()
        {
            // Arrange
            RequestEntity request = new RequestEntity
            {
                Id = Guid.NewGuid(),
                challenger = Guid.NewGuid(),
                defender = Guid.NewGuid(),
                challengerKDA = 3.5m,
                defenderKDA = 2.5m,
                winner = "",
                State = 0
            };

            _mockUnitOfWork.Setup(uow => uow.Requests).Returns(_mockRequestRepository.Object);
            _mockRequestRepository.Setup(repo => repo.Create(request)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);

            var requestService = new RequestService(_mockUnitOfWork.Object, _mockMapper.Object, _mockHubContext.Object);

            // Act
            RequestEntity addedRequest = await requestService.AddRequest(request);

            // Assert
            Assert.NotNull(addedRequest);
            Assert.Equal(addedRequest, request);
        }

        [Fact]
        public async Task UpdateRequestAsync_WithExistingRequest_ReturnsTrue()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var existingRequest = new RequestEntity
            {
                Id = requestId,
                challenger = Guid.NewGuid(),
                defender = Guid.NewGuid(),
                State = 1
            };

            var updatedRequest = new RequestEntity
            {
                Id = requestId,
                challenger = existingRequest.challenger,
                defender = existingRequest.defender,
                State = 0 
            };

            var defenderAccount = new LeagueUserEntity { Id = existingRequest.defender, UserName = "Defender", AccountId = "defenderAccountId" };
            var challengerAccount = new LeagueUserEntity { Id = existingRequest.challenger, UserName = "Challenger", AccountId = "challengerAccountId" };

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(requestId)).ReturnsAsync(existingRequest);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(existingRequest.defender)).ReturnsAsync(defenderAccount);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(existingRequest.challenger)).ReturnsAsync(challengerAccount);
            _mockMapper.Setup(m => m.Map(updatedRequest, existingRequest));
            _mockRequestRepository.Setup(r => r.Update(existingRequest)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);
            _mockHubClients.Setup(clients => clients.User(challengerAccount.AccountId)).Returns(_mockClientProxy.Object);

            var requestService = new RequestService(_mockUnitOfWork.Object, _mockMapper.Object, _mockHubContext.Object);

            // Act
            var result = await requestService.UpdateRequestAsync(updatedRequest);

            // Assert
            Assert.True(result);
            _mockUnitOfWork.Verify(uow => uow.Requests.GetById(requestId), Times.Once);
            _mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync("ReceiveNotification", It.Is<object[]>(args => args.Length == 1 && args[0].ToString() == "Defender has accepted your challenge!"), default),
                Times.Once
            );
        }

        [Fact]
        public async Task UpdateRequestAsync_WithNonExistingRequest_ReturnsFalse()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var updatedRequest = new RequestEntity { Id = requestId };

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(requestId))
                           .ReturnsAsync((RequestEntity)null);

            var requestService = new RequestService(_mockUnitOfWork.Object, _mockMapper.Object, _mockHubContext.Object);

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await requestService.UpdateRequestAsync(updatedRequest));

            // Assert
            Assert.Equal("Unable to retrieve request", exception.Message);
        }

        [Fact]
        public async Task GetAllReceived_WithValidId_ReturnsRequestObjects()
        {
            // Arrange
            string accountId = "exampleAccountId";
            var fakeLeagueUsers = new List<LeagueUserEntity>
            {
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "Test" },

            };
            var fakeRequests = new List<RequestEntity>
            {
                new RequestEntity
                {
                    Id = Guid.NewGuid(),
                    challenger = new Guid(),
                    defender = new Guid(),
                    challengerKDA = 3.5m,
                    defenderKDA = 2.5m,
                    winner = "",
                    State = 0,
                    AddedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Status = 1
                }
            };

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetAllFromAccount(accountId)).ReturnsAsync(fakeLeagueUsers);
            _mockUnitOfWork.Setup(uow => uow.Requests.GetAllReceived(It.IsAny<Guid>())).ReturnsAsync(fakeRequests);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(fakeRequests[0].challenger)).ReturnsAsync(fakeLeagueUsers[0]);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(fakeRequests[0].defender)).ReturnsAsync(fakeLeagueUsers[0]);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<RequestObject>>(It.IsAny<List<RequestObject>>()))
                       .Returns((List<RequestObject> requestObjects) =>
                       {
                           return requestObjects.Select(ro => new RequestObject
                           {
                               Id = ro.Id,
                               State = ro.State,
                               challenger = ro.challenger,
                               defender = ro.defender,
                               challengerKDA = ro.challengerKDA,
                               defenderKDA = ro.defenderKDA,
                               winner = ro.winner,
                               AddedDate = ro.AddedDate,
                               UpdateDate = ro.UpdateDate,
                               Status = ro.Status
                           });
                       });

            var requestService = new RequestService(_mockUnitOfWork.Object,_mockMapper.Object,_mockHubContext.Object);

            // Act
            var result = await requestService.GetAllReceived(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeRequests.Count(), result.Count());

        }

        [Fact]
        public async Task GetAllOngoing_WithValidId_ReturnsRequestObjects()
        {
            // Arrange
            string accountId = "exampleAccountId";
            var fakeLeagueUsers = new List<LeagueUserEntity>
            {
                new LeagueUserEntity { Id = Guid.NewGuid(), UserName = "Test" },

            };
            var fakeRequests = new List<RequestEntity>
            {
                new RequestEntity
                {
                    Id = Guid.NewGuid(),
                    challenger = new Guid(),
                    defender = new Guid(),
                    challengerKDA = 3.5m,
                    defenderKDA = 2.5m,
                    winner = "",
                    State = 1,
                    AddedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Status = 1
                }
            };

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetAllFromAccount(accountId)).ReturnsAsync(fakeLeagueUsers);
            _mockUnitOfWork.Setup(uow => uow.Requests.GetAllOngoing(It.IsAny<Guid>())).ReturnsAsync(fakeRequests);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(fakeRequests[0].challenger)).ReturnsAsync(fakeLeagueUsers[0]);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(fakeRequests[0].defender)).ReturnsAsync(fakeLeagueUsers[0]);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<RequestObject>>(It.IsAny<List<RequestObject>>()))
                       .Returns((List<RequestObject> requestObjects) =>
                       {
                           return requestObjects.Select(ro => new RequestObject
                           {
                               Id = ro.Id,
                               State = ro.State,
                               challenger = ro.challenger,
                               defender = ro.defender,
                               challengerKDA = ro.challengerKDA,
                               defenderKDA = ro.defenderKDA,
                               winner = ro.winner,
                               AddedDate = ro.AddedDate,
                               UpdateDate = ro.UpdateDate,
                               Status = ro.Status
                           });
                       });

            var requestService = new RequestService(_mockUnitOfWork.Object, _mockMapper.Object, _mockHubContext.Object);

            // Act
            var result = await requestService.GetAllOngoing(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeRequests.Count(), result.Count());

        }
    }




}



