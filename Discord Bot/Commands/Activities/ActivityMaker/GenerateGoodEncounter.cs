using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
    class GenerateGoodEncounter
    {
        public class CharData
        {

            public ulong Id { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public int Ap { get; set; }
            public int Dp { get; set; }
            public long Silver { get; set; }
           

        }
        public  async Task<string> GoodEncounterAsync(ulong id, int Trash)
        {

            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);
            var match =  await collection.Find(filter).FirstOrDefaultAsync();
            if(match != null)
            {
                var previousSilver = match.Silver;
                long gain = Convert.ToInt64(Trash);
                var updated = Builders<CharData>.Update.Set("Silver", previousSilver+gain);
                await collection.UpdateOneAsync(filter, updated);
                string encounter = $"You made {gain/1000000}m silver";
                return encounter;

            }
            else
            {
                return "404";
            }
        }
    }
}
