using BrewingBuddies_DataService.Data;
using BrewingBuddies_DataService.Repositories;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntergrationTests
{
    public class RequestRepositoryTest : IDisposable
    {
        private AppDbContext _context;
        private RequestRepository _requestRepository;
        private Mock<ILogger<RequestRepository>> _mockLogger;
        public RequestRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            Data();

            _mockLogger = new Mock<ILogger<RequestRepository>>();
            _requestRepository = new RequestRepository(_context, _mockLogger.Object);
        }

        private void Data()
        {
            RequestEntity defaultRequest1 = new RequestEntity
            {
                Id = new Guid("01358d6f-0eb6-465d-8f41-480510de6302"),
                State = 0,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = 1.00m,
                defenderKDA = 1.00m,
                winner = "bob",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };



            _context.Request.Add(defaultRequest1);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllRequestPendingById_Returns_CorrectRequests()
        {
            RequestEntity pendingRequest = new RequestEntity
            {
                Id = new Guid("b2fdd0bf-842f-4a9f-a199-1066344c76d8"),
                State = 0,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = .00m,
                defenderKDA = 0.00m,
                winner = "",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            await _context.Request.AddAsync(pendingRequest);
            await _context.SaveChangesAsync();

            var result = await _requestRepository.GetAllPending(pendingRequest.challenger);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.challenger == pendingRequest.challenger);
        }

        [Fact]
        public async Task GetAllRequestReceivedById_Returns_CorrectRequests()
        {
            RequestEntity receivedRequest = new RequestEntity
            {
                Id = new Guid("b2fdd0bf-842f-4a9f-a199-1066344c76d8"),
                State = 0,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = .00m,
                defenderKDA = 0.00m,
                winner = "",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            await _context.Request.AddAsync(receivedRequest);
            await _context.SaveChangesAsync();

            var result = await _requestRepository.GetAllReceived(receivedRequest.defender);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.defender == receivedRequest.defender);
        }

        [Fact]
        public async Task GetAllRequestOngoingById_Returns_CorrectRequests()
        {
            RequestEntity ongoinggRequest = new RequestEntity
            {
                Id = new Guid("b2fdd0bf-842f-4a9f-a199-1066344c76d8"),
                State = 1,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = .00m,
                defenderKDA = 0.00m,
                winner = "",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            await _context.Request.AddAsync(ongoinggRequest);
            await _context.SaveChangesAsync();

            var result = await _requestRepository.GetAllOngoing(ongoinggRequest.defender);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Contains(result, u => u.defender == ongoinggRequest.defender);
        }

        [Fact]
        public async Task GetAllRequestCompleteById_Returns_CorrectRequests()
        {
            RequestEntity completeRequest = new RequestEntity
            {
                Id = new Guid("b2fdd0bf-842f-4a9f-a199-1066344c76d8"),
                State = 2,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = .00m,
                defenderKDA = 0.00m,
                winner = "",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            await _context.Request.AddAsync(completeRequest);
            await _context.SaveChangesAsync();

            var result = await _requestRepository.GetAllComplete(completeRequest.defender);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Contains(result, u => u.defender == completeRequest.defender);
        }

        [Fact]
        public async Task AddRequest_SaveRequestToDatabase()
        {
            RequestEntity newRequest = new RequestEntity
            {
                Id = new Guid("b2fdd0bf-842f-4a9f-a199-1066344c76d8"),
                State = 2,
                challenger = new Guid("3faf25a1-5dcf-4281-abc2-0078a425c43b"),
                defender = new Guid("4325de51-c003-4659-853f-519b526d57e3"),
                challengerKDA = .00m,
                defenderKDA = 0.00m,
                winner = "",
                Status = 1,
                AddedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            _requestRepository.Create(newRequest); 

            var expectedRequest = await _context.Request.FindAsync(newRequest.Id);

            Assert.NotNull(expectedRequest);

        }

        [Fact]
        public async Task UpdateRequest_SaveRequestToDatabase()
        {
            Guid requestID = new Guid("01358d6f-0eb6-465d-8f41-480510de6302");

            RequestEntity ongoingRequest = _context.Request.FirstOrDefault(c => c.Id == requestID);

            Assert.NotNull(ongoingRequest);

            ongoingRequest.challengerKDA = 1.00m;
            ongoingRequest.defenderKDA = 1.00m;

            await _requestRepository.Update(ongoingRequest);

            RequestEntity expectedRequest = _context.Request.FirstOrDefault(u => u.Id == ongoingRequest.Id);

            Assert.NotNull(expectedRequest);
            Assert.Equal(expectedRequest.challengerKDA, ongoingRequest.challengerKDA);
            Assert.Equal(expectedRequest.defenderKDA, ongoingRequest.defenderKDA);
            Assert.Equal(expectedRequest, ongoingRequest);
        }

        [Fact]
        public async Task DeleteRequest_DeleteRequestFromDatabas()
        {
            Guid requestID = new Guid("01358d6f-0eb6-465d-8f41-480510de6302");

            await _requestRepository.DeleteRequest(requestID);

            RequestEntity deletedRequest = _context.Request.FirstOrDefault(u => u.Id == requestID);

            Assert.Equal(deletedRequest.Status, 0);
        }

    }
}
