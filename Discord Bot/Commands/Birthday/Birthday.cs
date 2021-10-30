using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Birthday
{
    class Birthday : BaseCommandModule
    {
        public class BirthdayData
        {
            
            public ulong Id { get; set; }
            public DateTime BirthdayDate { get; set; }
            
            

        }

        [Command("birthday")]
        [Description("Set your birthday date")]
        public async Task BirthdayCommand(CommandContext ctx,int day , int month , int year)
        {
            
            var id = ctx.Member.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<BirthdayData>("Birthday");
            var filter = Builders<BirthdayData>.Filter.Eq("_id", id);
            
            var ListOfAddedBirthdays = collection.Find(filter).FirstOrDefaultAsync().Result;
            if (ListOfAddedBirthdays == null)
            {
                var BirthdayDateSet = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
                DateTime TempDate;
                if (!DateTime.TryParse(BirthdayDateSet.ToString(),out TempDate))
                {
                    await ctx.RespondAsync("You gotta be kidding me");
                    return;
                }
                await collection.InsertOneAsync(new BirthdayData{ Id = id , BirthdayDate = BirthdayDateSet});
                
            }
            else
            {
                await ctx.RespondAsync("You already set your birthday");
            }



        }

    }
}
