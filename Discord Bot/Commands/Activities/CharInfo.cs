using Discord_Bot.Attributes;
using Discord_Bot.Commands.Activities.ActivityMaker;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;


namespace Discord_Bot.Commands.Activities
{
    class CharInfo : BaseCommandModule
    {

        public class CharData
        {

            public ulong Id { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public int Ap { get; set; }
            public int Dp { get; set; }
            public int Silver { get; set; }

        }

        


        [Command("charinfo")]
        [Description("Character info")]
        [Category("activity")]
        public async Task Info(CommandContext ctx)
        {
            var id = ctx.Member.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);

            var match =  await collection.Find(filter).FirstOrDefaultAsync();
            if (match == null)
            {
                await ctx.RespondAsync("You havent created char yet");
            }
            else
            {
                
                var info = new ClassStats().GetStats(match.Class);
             
               
                
                
                var ap = (match.Ap + Convert.ToInt64(info["Ap"])).ToString();
               
                var dp = (match.Dp + Convert.ToInt64(info["Dp"])).ToString();
                var embed = new DiscordEmbedBuilder()
                    .AddField("Character name", match.Name, false)
                    .AddField("Class", info["Classname"], false)
                    .AddField("Ap", ap.ToString(), false)
                    .AddField("Dp", dp.ToString(), false);
                 if(match.Silver > 1000000000)
                {
                    embed.AddField("Silver", $"{match.Silver /1000000000}b", false);
                }
                else
                {
                     embed.AddField("Silver", $"{match.Silver / 1000000}m", false);
                }
                  
                   
                
               
                
                await ctx.RespondAsync(embed);
            }
            
        }
    }
}
