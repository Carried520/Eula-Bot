using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
    class GenerateBadEncounter
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


        public  async Task<string> BadEncounterAsync(ulong id,int Trash)
        {
            string encounter = "";
            
            double multiplier = 1;
            
            var random = new Random().Next(0,6);
            switch (random)
            {
                case 0:
                    encounter = "During grind your cpu overheated -12.5% trash";
                    multiplier = 0.875;
                    break;
                case 1:
                    encounter = "During grind you were griefed -50% less trash";
                    multiplier = 0.5;
                    break;
                case 2:
                    encounter = "You tried to grind during night but mobs killed u and your jin vipers broke.You make trash but u have to spend all money from it on crystals";
                    multiplier = 0;
                    break;
                case 3:
                    encounter = "Some top nw player kicked you off the spot u gain -75% trash";
                    multiplier = 0.25;
                    break;
                case 4:
                    encounter = "You didnt learn rotation properly -25% trash";
                    multiplier = 0.75;
                    break;
                case 5:
                    encounter = "You turned off fps cap and your mighty rtx 3090 has blown up -100% trash.";
                    multiplier = 0;
                    break;
                

            }
            long gain = Convert.ToInt64(multiplier *Trash);
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);
            var match = await collection.Find(filter).FirstOrDefaultAsync();
           
            if (match != null)
            {
                encounter += "" + $" You gained {gain/1000000}m silver";
                var previousSilver = match.Silver;
                var updated = Builders<CharData>.Update.Set("Silver", gain+previousSilver);
                await collection.UpdateOneAsync(filter, updated);
                return encounter;
            }
            else
            {
                return "404";
            }
            
        }
    }
}
