using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BrewingBuddies_BLL.Interfaces.Repositories;
using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BrewingBuddies_RiotClient
{
    public class API_Request : IRiotAPIRepository
    {
        public static async Task<AccountDTO> GetUuid(string gameName, string tagLine, string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrll = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(apiUrll);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        AccountDTO? dataObject = JsonConvert.DeserializeObject<AccountDTO>(responseData);

                        return dataObject;

                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");

                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");

                    return null;
                }
            }
        }

        public async Task<string> GetSummoner(string gameName, string tagLine, string apiKey)
        {
            var Account = await GetUuid(gameName, tagLine, apiKey);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{Account.puuid}?api_key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        SummonerDTO? dataObject = JsonConvert.DeserializeObject<SummonerDTO>(responseData);
                        if (dataObject != null)
                        {
                            dataObject.summonerName = Account.gameName;
                            dataObject.tagLine = Account.tagLine;
                        }
                        string json = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
                        return json;
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"An error occurred: {ex.Message}");
                     return null;
                }

            }
        }

        public async Task<List<string>> GetMatchIDs(string api_key, string puuid)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start=0&count=7";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-Riot-Token", api_key);

                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<string>>(responseData);
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }
        }
        public async Task<DateTime?> GetMatchStartTimeAsync(string apiKey, string matchId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        JObject matchData = JObject.Parse(responseData);

                        long gameStartTimestamp = matchData["info"]["gameStartTimestamp"].Value<long>();

                        DateTime matchStartTime = DateTimeOffset.FromUnixTimeMilliseconds(gameStartTimestamp).UtcDateTime;

                        return matchStartTime;
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<decimal?> GetKdaAsync(string apiKey, string matchId, string summonerId)
        {
            try
            {
                string endpoint = $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}";
                using (HttpClient client = new HttpClient())
                {


                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        JObject matchData = JObject.Parse(responseData);
                        JArray participants = (JArray)matchData["info"]["participants"];

                        foreach (var participant in participants)
                        {
                            if (participant["puuid"].ToString() == summonerId)
                            {
                                int kills = participant["kills"].ToObject<int>();
                                int deaths = participant["deaths"].ToObject<int>();
                                int assists = participant["assists"].ToObject<int>();

                                decimal kda = deaths == 0 ? (kills + assists) : (decimal)(kills + assists) / deaths;
                                return Decimal.Round(kda,2);
                            }
                        }

                        throw new Exception("PUUID not found in the match data.");
                    }
                    throw new Exception("de zak");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching match data: {ex.Message}");
                return -1;
            }
        }



    }
}
