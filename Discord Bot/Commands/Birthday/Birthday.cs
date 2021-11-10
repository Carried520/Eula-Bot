using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Birthday
{
    class Birthday : BaseCommandModule
    {
        
        public class BirthdayData
        {
            
            public ulong Id { get; set; }
            public DateTime BirthdayDate { get; set; }
            public ulong BirthdayGuild { get; set; }
            public ulong BirthdayChannel { get; set; }
            




        }

        [Command("birthday")]
        [Description("Set your birthday date")]
        [Category("rp")]
        [Cooldown(1, 2592000, CooldownBucketType.User)]
        public async Task BirthdayCommand(CommandContext ctx,int day , int month , int year)
        {
            
            var id = ctx.Member.Id;
            var guild = ctx.Guild.Id;
            var channel = ctx.Channel.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<BirthdayData>("Birthday");
            var filter = Builders<BirthdayData>.Filter.Eq("_id", id);
            
            var ListOfAddedBirthdays = await collection.Find(filter).FirstOrDefaultAsync();
            if (ListOfAddedBirthdays == null)
            {
                var BirthdayDateSet = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
                DateTime TempDate;
                if (!DateTime.TryParse(BirthdayDateSet.ToString(),out TempDate))
                {
                    await ctx.RespondAsync("You gotta be kidding me");
                    return;
                }
                await collection.InsertOneAsync(new BirthdayData{ Id = id , BirthdayDate = BirthdayDateSet,BirthdayGuild = guild,BirthdayChannel = channel});
                
            }
            else
            {
                var BirthdayDateSet = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
                DateTime TempDate;
                if (!DateTime.TryParse(BirthdayDateSet.ToString(), out TempDate))
                {
                    await ctx.RespondAsync("You gotta be kidding me");
                    return;
                }
                var updated = Builders<BirthdayData>.Update.Set("BirthdayDate",BirthdayDateSet);
                await collection.UpdateOneAsync(filter,updated);
                await ctx.Channel.SendMessageAsync($"Birthday updated to {BirthdayDateSet.Date.ToShortDateString()}");
            }



        }

    }
}
