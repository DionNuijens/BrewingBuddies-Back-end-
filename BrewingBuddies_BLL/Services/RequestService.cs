using AutoMapper;
using BrewingBuddies_BLL.Hubs;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrewingBuddies_BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hub;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hub = hub;
        }

 

        public async Task<RequestEntity> AddRequest(RequestEntity request)
        {

            request.winner = "";
            request.State = 0;

            if (request.defender == Guid.Empty || request.challenger == Guid.Empty)
            {
                throw new ArgumentNullException("Defender and/or Challenger cannot be null."); 
            }
            await _unitOfWork.Requests.Create(request);
            await _unitOfWork.CompleteAsync();

            return request;
        }

        public async Task<IEnumerable<RequestObject>> GetAllPending(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if (loop == null)
            {
                throw new InvalidOperationException($"Undable to get users");

            }
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllPending(item.Id);
                if (users == null)
                {
                    throw new InvalidOperationException($"Undable to get {item.UserName} pending requests");

                }
                foreach (var user in users)
                {
                    var challengerUser = await _unitOfWork.LeagueUsers.GetById(user.challenger); 
                    var defenderUser = await _unitOfWork.LeagueUsers.GetById(user.defender); 

                    var requestObject = new RequestObject
                    {
                        Id = user.Id,
                        State = user.State,
                        challenger = challengerUser?.UserName, 
                        defender = defenderUser?.UserName, 
                        challengerKDA = user.challengerKDA,
                        defenderKDA = user.defenderKDA,
                        winner = user.winner,
                        AddedDate = user.AddedDate,
                        UpdateDate = user.UpdateDate,
                        Status = user.Status
                    };

                    requestObjects.Add(requestObject);
                }

            }
            return _mapper.Map<IEnumerable<RequestObject>>(requestObjects);

        }

        public async Task<IEnumerable<RequestObject>> GetAllReceived(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if (loop == null)
            {
                throw new InvalidOperationException($"Undable to get users");

            }
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllReceived(item.Id);
                if (users == null)
                {
                    throw new InvalidOperationException($"Undable to get {item.UserName} Received requests");

                }

                foreach (var user in users)
                {
                    var challengerUser = await _unitOfWork.LeagueUsers.GetById(user.challenger); 
                    var defenderUser = await _unitOfWork.LeagueUsers.GetById(user.defender); 

                    var requestObject = new RequestObject
                    {
                        Id = user.Id,
                        State = user.State,
                        challenger = challengerUser?.UserName, 
                        defender = defenderUser?.UserName, 
                        challengerKDA = user.challengerKDA,
                        defenderKDA = user.defenderKDA,
                        winner = user.winner,
                        AddedDate = user.AddedDate,
                        UpdateDate = user.UpdateDate,
                        Status = user.Status
                    };

                    requestObjects.Add(requestObject);
                }

            }
            return _mapper.Map<IEnumerable<RequestObject>>(requestObjects);

        }

        public async Task<IEnumerable<RequestObject>> GetAllOngoing(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if (loop == null)
            {
                throw new InvalidOperationException($"Undable to get users");

            }
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllOngoing(item.Id);
                if (users == null)
                {
                    throw new InvalidOperationException($"Undable to get {item.UserName} Ongoing requests");

                }

                foreach (var user in users)
                {
                    var challengerUser = await _unitOfWork.LeagueUsers.GetById(user.challenger); 
                    var defenderUser = await _unitOfWork.LeagueUsers.GetById(user.defender); 

                    var requestObject = new RequestObject
                    {
                        Id = user.Id,
                        State = user.State,
                        challenger = challengerUser?.UserName, 
                        defender = defenderUser?.UserName, 
                        challengerKDA = user.challengerKDA,
                        defenderKDA = user.defenderKDA,
                        winner = user.winner,
                        AddedDate = user.AddedDate,
                        UpdateDate = user.UpdateDate,
                        Status = user.Status
                    };

                    requestObjects.Add(requestObject);
                }

            }
            return _mapper.Map<IEnumerable<RequestObject>>(requestObjects);

        }
        public async Task<IEnumerable<RequestObject>> GetAllComplete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Account ID is not filled in."); 
            }
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            if (loop == null)
            {
                throw new InvalidOperationException($"Undable to get users");

            }
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllComplete(item.Id);
                if (users == null)
                {
                    throw new InvalidOperationException($"Undable to get {item.UserName} Complete requests");

                }

                foreach (var user in users)
                {
                    var challengerUser = await _unitOfWork.LeagueUsers.GetById(user.challenger); 
                    var defenderUser = await _unitOfWork.LeagueUsers.GetById(user.defender); 

                    var requestObject = new RequestObject
                    {
                        Id = user.Id,
                        State = user.State,
                        challenger = challengerUser?.UserName, 
                        defender = defenderUser?.UserName, 
                        challengerKDA = user.challengerKDA,
                        defenderKDA = user.defenderKDA,
                        winner = user.winner,
                        AddedDate = user.AddedDate,
                        UpdateDate = user.UpdateDate,
                        Status = user.Status
                    };

                    requestObjects.Add(requestObject);
                }

            }
            return _mapper.Map<IEnumerable<RequestObject>>(requestObjects);

        }

        public async Task<bool> UpdateRequestAsync(RequestEntity request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentNullException("Request ID is not filled in."); 
            }

            var existingRequest = await _unitOfWork.Requests.GetById(request.Id);

            if (existingRequest == null)
                throw new InvalidOperationException("Unable to retrieve request");

            //_mapper.Map(request, existingRequest);

            await _unitOfWork.Requests.Update(request);
            await _unitOfWork.CompleteAsync();
            if (request.State == 0)
            {
                var defenderAccount = await _unitOfWork.LeagueUsers.GetById(existingRequest.defender);
                var challengerAccount = await _unitOfWork.LeagueUsers.GetById(existingRequest.challenger);
                await _hub.Clients.User(challengerAccount.AccountId).SendAsync("ReceiveNotification", $"{defenderAccount.UserName} has accepted your challenge!");
            }
            return true;
        }


        public async Task<bool> DeleteRuest(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("Request ID is not filled in."); 
            }
            var request = await _unitOfWork.Requests.GetById(userId);

            if (request == null)
                throw new InvalidOperationException("Unable To retrieve Request");

            await _unitOfWork.Requests.DeleteRequest(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }

    }
}
