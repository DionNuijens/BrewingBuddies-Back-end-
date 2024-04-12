using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_API_Requests
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string gameName = "LostBorders";
            string tagLine = "EUW";
            string apiKey = "RGAPI-7e5c83ae-70f6-4437-a401-18da20d7b2a4";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if(response.IsSuccessStatusCode)
                    {
                        string responsData = await response.Content.ReadAsStringAsync();
                        Account dataObject = JsonConvert.DeserializeObject<Account>(responsData);

                        Console.WriteLine("API Response:");
                        Console.WriteLine(dataObject.puuid);
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
