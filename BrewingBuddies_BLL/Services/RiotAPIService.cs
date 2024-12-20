﻿using AutoMapper;
using AutoMapper.Internal;
using BrewingBuddies_BLL.Hubs;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_BLL.Interfaces.Services;
using BrewingBuddies_Entitys;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_BLL.Services
{
    public class RiotAPIService : IRiotAPIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IRiotAPIRepository _riotAPIRepository;

        public RiotAPIService(IUnitOfWork unitOfWork, IMapper mapper, IRiotAPIRepository riotAPI)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _riotAPIRepository = riotAPI;
        }



        public async Task<string> GetSummonerAsync(string gameName, string tagLine, string apiKey)
        {
            try
            {
                var playerData = await _riotAPIRepository.GetSummoner(gameName, tagLine, apiKey);
                return playerData;
            }
            catch (Exception ex)
            {
                throw; 
            }
        }


        public async Task<bool> UpdateOngoingChallenge(Guid id, string api_key)
        {


            try
            {
                int MatchCounter = 0;
                var Challenge = await _unitOfWork.Requests.GetById(id);
                var challenger = await _unitOfWork.LeagueUsers.GetById(Challenge.challenger);
                var defender = await _unitOfWork.LeagueUsers.GetById(Challenge.defender);

                List<string> challengerMatches = await _riotAPIRepository.GetMatchIDs(api_key, challenger.RiotId);
                List<string> defenderMatches = await _riotAPIRepository.GetMatchIDs(api_key, defender.RiotId);

                foreach (var item in challengerMatches)
                {
                    DateTime? matchStartTime = await _riotAPIRepository.GetMatchStartTimeAsync(api_key, item);

                    if (matchStartTime.HasValue && matchStartTime.Value < Challenge.UpdateDate)
                    {
                        break;
                    }
                    MatchCounter++;
                }

                if (MatchCounter == 0) 
                {
                    return false; 
                }

                decimal? challengerKDA = await _riotAPIRepository.GetKdaAsync(api_key, challengerMatches[MatchCounter - 1], challenger.RiotId);
                Challenge.challengerKDA = challengerKDA ?? 0m;

                MatchCounter = 0;

                foreach (var item in defenderMatches)
                {
                    DateTime? matchStartTime = await _riotAPIRepository.GetMatchStartTimeAsync(api_key, item);

                    if (matchStartTime.HasValue && matchStartTime.Value < Challenge.UpdateDate)
                    {
                        break;
                    }
                    MatchCounter++;
                }

                if (MatchCounter == 0)
                {
                    return false;
                }

                decimal? defenderKDA = await _riotAPIRepository.GetKdaAsync(api_key, defenderMatches[MatchCounter - 1], defender.RiotId);
                Challenge.defenderKDA = defenderKDA ?? 0m;

                if(challengerKDA == 0.00m || defenderKDA == 0.00m)
                {
                    return false;
                }

                if (challengerKDA > defenderKDA)
                {
                    Challenge.winner = challenger.UserName;
                }
                else if (defenderKDA > challengerKDA)
                {
                    Challenge.winner = defender.UserName;
                }
                else
                {
                    Challenge.winner = "niemand";
                }
                await _unitOfWork.Requests.Update(Challenge);
                await _unitOfWork.CompleteAsync();
                    

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
