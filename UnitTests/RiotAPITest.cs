using AutoMapper;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Services;
using BrewingBuddies_Entitys;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class RiotAPITest
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRiotAPIRepository> _mockRiotAPIRepository;

        public RiotAPITest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockRiotAPIRepository = new Mock<IRiotAPIRepository>();
        }

        [Fact]
        public async Task GetSummonerAsync_ValidInput_ReturnsPlayerData()
        {
            // Arrange

            var service = new RiotAPIService(_mockUnitOfWork.Object, _mockMapper.Object, _mockRiotAPIRepository.Object);

            string gameName = "SampleGameName";
            string tagLine = "1234";
            string apiKey = "SampleAPIKey";
            string expectedPlayerData = "SamplePlayerData";

            _mockRiotAPIRepository.Setup(repo => repo.GetSummoner(gameName, tagLine, apiKey))
                           .ReturnsAsync(expectedPlayerData);

            // Act
            var result = await service.GetSummonerAsync(gameName, tagLine, apiKey);

            // Assert
            Assert.Equal(expectedPlayerData, result);
        }


        [Fact]
        public async Task GetSummonerAsync_ExceptionInRepository_ThrowsException()
        {
            // Arrange


            var service = new RiotAPIService(_mockUnitOfWork.Object, _mockMapper.Object, _mockRiotAPIRepository.Object);

            string gameName = "SampleGameName";
            string tagLine = "1234";
            string apiKey = "SampleAPIKey";

            _mockRiotAPIRepository.Setup(repo => repo.GetSummoner(gameName, tagLine, apiKey))
                           .ThrowsAsync(new Exception("Simulated repository exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetSummonerAsync(gameName, tagLine, apiKey));
        }


        [Fact]
        public async Task UpdateOngoingChallenge_ValidIdAndApiKey_ReturnsTrue()
        {
            // Arrange


            var service = new RiotAPIService(_mockUnitOfWork.Object, _mockMapper.Object, _mockRiotAPIRepository.Object);

            Guid id = Guid.NewGuid();
            string apiKey = "SampleAPIKey";

            var challenge = new RequestEntity 
            {
                Id = id,
                challenger = Guid.NewGuid(), 
                defender = Guid.NewGuid(), 
                UpdateDate = DateTime.UtcNow 
            };

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(id))
                          .ReturnsAsync(challenge);

            var challengerUser = new LeagueUserEntity 
            {
                RiotId = "ChallengerRiotId" 
            };

            var defenderUser = new LeagueUserEntity 
            {
                RiotId = "DefenderRiotId" 
            };

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(challenge.challenger)).ReturnsAsync(challengerUser);
            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(challenge.defender)).ReturnsAsync(defenderUser);

            _mockRiotAPIRepository.Setup(repo => repo.GetMatchIDs(apiKey, challengerUser.RiotId)).ReturnsAsync(new List<string> { "MatchID1", "MatchID2" });

            _mockRiotAPIRepository.Setup(repo => repo.GetMatchIDs(apiKey, defenderUser.RiotId)).ReturnsAsync(new List<string> { "MatchID3", "MatchID4" });

            decimal challengerKDA = 3.0m; 
            decimal defenderKDA = 2.5m; 

            _mockRiotAPIRepository.Setup(repo => repo.GetKdaAsync(apiKey, It.IsAny<string>(), challengerUser.RiotId)).ReturnsAsync(challengerKDA);

            _mockRiotAPIRepository.Setup(repo => repo.GetKdaAsync(apiKey, It.IsAny<string>(), defenderUser.RiotId)).ReturnsAsync(defenderKDA);

            _mockUnitOfWork.Setup(uow => uow.Requests.Update(challenge)).ReturnsAsync(true);

            // Act
            var result = await service.UpdateOngoingChallenge(id, apiKey);

            // Assert
            Assert.True(result);
            Assert.Equal(challengerUser.UserName, challenge.winner); 
        }

        [Fact]
        public async Task UpdateOngoingChallenge_ChallengerUserNotFound_ReturnsFalse()
        {
            // Arrange

            var service = new RiotAPIService(_mockUnitOfWork.Object, _mockMapper.Object, _mockRiotAPIRepository.Object);

            Guid id = Guid.NewGuid();
            string apiKey = "SampleAPIKey";

            var challenge = new RequestEntity 
            {
                Id = id,
                challenger = Guid.NewGuid(), 
                defender = Guid.NewGuid(), 
                UpdateDate = DateTime.UtcNow 
            };

            _mockUnitOfWork.Setup(uow => uow.Requests.GetById(id))
                          .ReturnsAsync(challenge);

            _mockUnitOfWork.Setup(uow => uow.LeagueUsers.GetById(challenge.challenger))
                          .ReturnsAsync((LeagueUserEntity)null); 

            // Act
            var result = await service.UpdateOngoingChallenge(id, apiKey);

            // Assert
            Assert.False(result);
           
        }
    }
}
