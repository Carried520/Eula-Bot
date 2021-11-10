using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Utils
{
    class SpotifyAuth
    {
        public async Task<string> GetAuth()
        {
            var ClientId = "04c7d1f8a8434ab8aa6f59c1df922556";
            var ClientSecret = "a56fd1c8d5944b668c76d341ce1eae7c";
            var client = new HttpClient();
            
            var byteArray = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic " + byteArray); 
            var data = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var success = await client.PostAsync("https://accounts.spotify.com/api/token", data);
            var token = success.Content.ReadAsStringAsync().Result;
            JsonDocument json = JsonDocument.Parse(token);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty("access_token");
            return results.GetString();
        }
    }
}
