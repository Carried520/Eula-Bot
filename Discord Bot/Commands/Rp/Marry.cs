using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Marry : BaseCommandModule
    {


        public class MarryStatus
        {

            public ulong Id { get; set; }
            public ulong Partner { get; set; }
            
        }


        [Command("marry")]
        [Description("marry someone")]
        [Category("rp")]

        public async Task MarryCommand (CommandContext ctx , DiscordMember member)
        {

            var id = ctx.Member.Id;
            var MemberId = member.Id;
            if(id == MemberId)
            {
                await ctx.RespondAsync("You cant marry yourself");
                return;
            }

            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<MarryStatus>("Marry");
            var filter = Builders<MarryStatus>.Filter.Eq("_id", id);
            var SecondFilter = Builders<MarryStatus>.Filter.Eq("_id", MemberId);
            var list = collection.Find(filter).FirstOrDefaultAsync().Result;
            var SecondList = collection.Find(SecondFilter).FirstOrDefaultAsync().Result;

            if(list == null && SecondList == null) {

                await ctx.RespondAsync($" {member.Mention} Respond with *agree* to continue.");
                var result = await ctx.Channel.GetNextMessageAsync(m=> {
                    return m.Content.ToLower() == "agree";
                });
                if (!result.TimedOut && result.Result.Author == member)
                {
                    await ctx.Channel.SendMessageAsync($" {member.Mention} You may now kiss the bride(Respond with  *kiss*)");
                    var kissed = await ctx.Channel.GetNextMessageAsync(m => {
                        return m.Content.ToLower() == "kiss";
                    });
                    if(!kissed.TimedOut && kissed.Result.Author == member)
                    {

                        string webClient = new WebClient().DownloadString("https://purrbot.site/api/img/sfw/kiss/gif");
                        JsonDocument json = JsonDocument.Parse(webClient);
                        JsonElement root = json.RootElement;
                        JsonElement results = root.GetProperty("link");
                        var embed = new DiscordEmbedBuilder()
                            .WithDescription($"{ctx.Member.Mention} marries {member.Mention}")
                            .WithImageUrl(results.GetString());
                        await ctx.Channel.SendMessageAsync(embed.Build());
                        var marriages = new List<MarryStatus> {

                            new MarryStatus{Id = id,Partner = MemberId},
                            new MarryStatus{Id = MemberId,Partner = id}
                        };
                        await collection.InsertManyAsync(marriages);
                    }
                    
                        

                  
                }
            }
            else
            {
                await ctx.RespondAsync("One of you is already married");
            }

            
            
        }
    }
}
