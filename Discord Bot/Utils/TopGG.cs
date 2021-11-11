using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Discord_Bot.Services
{
    class TopGG
    {

        class Stats
        {
            public int server_count { get; set; }
            public int shard_count { get; set; }

        }
        public async Task Update( int ServerCount,int ShardCount)
        {


            var LocalStats = new Stats { server_count = ServerCount, shard_count = ShardCount};
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"{Config.Get("topgg")}");
            var content  = JsonSerializer.Serialize(LocalStats);
            var data = new StringContent(content,Encoding.UTF8, "application/json");
            var success = await client.PostAsync("https://top.gg/api/bots/713127586976366604/stats", data);
                
            
        }
    }
}
