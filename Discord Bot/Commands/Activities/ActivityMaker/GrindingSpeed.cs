using DSharpPlus.CommandsNext;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
    public class GrindingSpeed
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









        public int Grind(double ap,double dp,string GrindSpot)
        {
            int[] stats = new GrindingSpots().ReturnSpotStats(GrindSpot);
            int RequiredAp = stats[0];
            _ = stats[1];
            double grindResult = ap / RequiredAp * stats[2] * stats[3];
            double roundGrindResult = Math.Round(grindResult);

            int grindData = Convert.ToInt32(roundGrindResult);

            return grindData;
        }

        public async Task<string[]> BadGrindAsync(CommandContext ctx,double RequiredAp,double RequiredDp,double ap,double dp,string GrindSpot)
        {
            int[] stats = new GrindingSpots().ReturnSpotStats(GrindSpot);
            string[] grindData = new string[2];
            double ChanceToDie = (ap + dp) / (RequiredDp + RequiredAp) * 100;
            int random = new Random().Next(0, 100);
            double roundGrindResult;
            

            double grindResult;
            if (random < ChanceToDie)
            {
                double Amount = new Random().Next(10,50);
                Amount = Amount / 100;
                grindResult = ap / RequiredAp * stats[2] * stats[3] * Amount;
                roundGrindResult = Convert.ToInt64(Math.Floor(grindResult));
                grindData[0] = $"Because you didn't have enough ap or dp you died but still you made {Math.Round(roundGrindResult/1000000)}m silver";
                grindData[1] = $"{roundGrindResult}";

            }
            else
            {
                double rand = new Random().Next(60, 90);
                rand = rand / 100;
                grindResult = Convert.ToDouble(ap / RequiredAp) * stats[2] * stats[3] * rand;
                Console.WriteLine(grindResult);
                roundGrindResult = Convert.ToInt64(Math.Floor(grindResult));
                grindData[0] = $" You didn't have enough ap or dp  but still you survived and made {Math.Round(roundGrindResult/1000000)}m silver";
                grindData[1] = $"{roundGrindResult}";
                Console.WriteLine(roundGrindResult);

            }


            var id = ctx.Member.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);
            var match = collection.Find(filter).FirstOrDefaultAsync().Result;
            if (match != null)
            {
                var previousSilver = match.Silver;
                long gain = Convert.ToInt64(roundGrindResult);
                var updated = Builders<CharData>.Update.Set("Silver", previousSilver + gain);
                await collection.UpdateOneAsync(filter, updated);


            }
            
            return grindData;
        }
    }
}
