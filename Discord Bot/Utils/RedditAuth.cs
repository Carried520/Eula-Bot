using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Utils
{
    class RedditAuth
    {

        
        public async Task<string> GetAuth()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Eula Bot");
            var byteArray = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Config.get("reddit_client")}:{Config.get("reddit_secret")}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", byteArray);
            var data = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var success = await client.PostAsync("https://www.reddit.com/api/v1/access_token", data);
            var token = success.Content.ReadAsStringAsync().Result;
            JsonDocument json = JsonDocument.Parse(token);
            JsonElement root = json.RootElement;
            string results = root.GetProperty("access_token").ToString();
            return results;
        }
    }
}
