using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities.ActivityMaker
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



    class Grind : BaseCommandModule
    {
        [Command("grind")]
        [Description("grind mobs")]
        [Category("activity")]
        [Cooldown(1,300,CooldownBucketType.User)]
        public async Task GrindCommand(CommandContext ctx,string GrindSpot)
        {
            string[] spots = { "grassbeetles","imps","aakman","se","sycraia","orcs"};
            if (!spots.Contains(GrindSpot))
            {
                await ctx.RespondAsync("No such spot found now go wait fucker");
                return;
            }
            var id = ctx.Member.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);
            var match =  await collection.Find(filter).FirstOrDefaultAsync();
            if (match != null)
            {
                var info = new ClassStats().GetStats(match.Class);
                double ap = match.Ap + Convert.ToDouble(info["Ap"]);
                var dp = match.Dp + Convert.ToDouble(info["Dp"]);
                int[] stats = new GrindingSpots().ReturnSpotStats(GrindSpot);
                int RequiredAp = stats[0];
                int RequiredDp = stats[1];
                if (ap>=RequiredAp && dp >= RequiredDp)
                {
                    var random = new Random().Next(0, 100);
                    if (random > 25)
                    {
                        var CalculateTrash = new GrindingSpeed().Grind(ap, dp, GrindSpot.ToLower());
                        var msg = await new GenerateGoodEncounter().GoodEncounterAsync(ctx.Member.Id, CalculateTrash);
                        await ctx.RespondAsync(msg);
                    }
                    else
                    {
                        var CalculateTrash = new GrindingSpeed().Grind(ap, dp, GrindSpot.ToLower());
                        var msg = await new GenerateBadEncounter().BadEncounterAsync(ctx.Member.Id, CalculateTrash);
                        await ctx.RespondAsync(msg);
                    }
                }
                else
                {
                    var CalculateTrash = await new GrindingSpeed().BadGrindAsync(ctx,RequiredAp,RequiredDp,ap, dp, GrindSpot.ToLower());
                    var msg = CalculateTrash[0];
                    await ctx.RespondAsync(msg);
                }
                

            }
            else
            {
                await ctx.RespondAsync("You dont have class registered");
                return;
            }




        }



        






            [Command("spots")]
        [Description("list of spots")]
        [Category("activity")]
        
        public async Task GrindList (CommandContext ctx)
        {
            string[] spots = {  "grassbeetles","imps","aakman", "se", "sycraia", "orcs"};
            var stats = new GrindingSpots();
            var embed = new DiscordEmbedBuilder();
            var SpotBuilder = new StringBuilder();
            var StatsBuilder = new StringBuilder();
            var TrashBuilder = new StringBuilder();
            foreach(var spot in spots)
            {
                var SpotInfo = stats.ReturnSpotStats(spot);
                SpotBuilder.Append($"\n{spot}");
                StatsBuilder.Append($"\n{SpotInfo[0]}/{SpotInfo[1]}");
                TrashBuilder.Append($"\n{SpotInfo[2]}/{SpotInfo[3]}");
                
                
            }
            embed.
                AddField("spot", SpotBuilder.ToString(), true)
                .AddField("Ap/Dp", StatsBuilder.ToString(), true)
                .AddField("Avg Trash/Trash Value", TrashBuilder.ToString(), true);
            await ctx.RespondAsync(embed);
            

        }

        
    }
}
