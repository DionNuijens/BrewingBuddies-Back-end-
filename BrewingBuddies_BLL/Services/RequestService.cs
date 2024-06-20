using AutoMapper;
using BrewingBuddies_BLL.Hubs;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BrewingBuddies.Hub; 

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

        //public async Task NotifyUsersAsync(string message)
        //{
        //    await _hub.Clients.All.SendAsync("ReceiveNotification", message);
        //}

        public async Task<RequestEntity> AddRequest(RequestEntity request)
        {
            if(request.defender == null)
            {
                return null;
            }

            if (request.challenger == null)
            {
                return null;
            }

            request.winner = "";
            request.State = 0;

            await _unitOfWork.Requests.Create(request);
            await _unitOfWork.CompleteAsync();

            return request;
        }

        public async Task<IEnumerable<RequestObject>> GetAllPending(string id)
        {
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllPending(item.Id);

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
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllReceived(item.Id);

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
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllOngoing(item.Id);

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
            var loop = await _unitOfWork.LeagueUsers.GetAllFromAccount(id);
            var requestObjects = new List<RequestObject>();
            foreach (var item in loop)
            {
                var users = await _unitOfWork.Requests.GetAllComplete(item.Id);

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

            var existingRequest = await _unitOfWork.Requests.GetById(request.Id);

            if (existingRequest == null)
                return false;

            _mapper.Map(request, existingRequest);

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

        //public async Task<bool> UpdateRequestCompleteAsync(RequestEntity request)
        //{

        //    var existingRequest = await _unitOfWork.Requests.GetById(request.Id);

        //    if (existingRequest == null)
        //        return false;

        //    _mapper.Map(request, existingRequest);

        //    await _unitOfWork.Requests.Update(request);
        //    await _unitOfWork.CompleteAsync();

        //    return true;
        //}

        //public async Task<RequestEntity> GetRequestByIdAsync(Guid requestId)
        //{
        //    var request = await _unitOfWork.Requests.GetById(requestId);
        //    return request != null ? _mapper.Map<RequestEntity>(request) : null;
        //}

        public async Task<bool> DeleteRuest(Guid userId)
        {
            var user = await _unitOfWork.Requests.GetById(userId);

            if (user == null)
                return false;

            await _unitOfWork.Requests.DeleteRequest(userId);
            await _unitOfWork.CompleteAsync();

            return true;
        }

    }
}
