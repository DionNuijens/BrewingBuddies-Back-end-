using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewingBuddies_Entitys;
using Newtonsoft.Json;

namespace BrewingBuddies_RiotClient
{
    public class API_Request
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

        public static async Task<string> GetSummoner(string gameName, string tagLine, string apiKey)
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
        public static async Task<string> GetAccountInfo(string gameName, string tagLine, string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        return responseData;
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
    }
}
