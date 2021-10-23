using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class ExpCommand : BaseCommandModule
    {

        public class Exp
        {

            public ulong Id { get; set; }
            public int Experience { get; set; }
            public int Level { get; set; }
        }



        [Command("exp")]
        [Description("Shows your exp")]
        public async Task Experience(CommandContext ctx)
        {
            var id = ctx.Member.Id;



            var client = new MongoClient(Config.get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<Exp>("Exp");
            var filter = Builders<Exp>.Filter.Eq("_id", id);
            var list = collection.Find(filter).FirstOrDefaultAsync().Result;


            if (list == null)
            {
               
                await ctx.Channel.SendMessageAsync($"No user data ");
            }

            else
            {
                var lvl = list.Level;
                var RequiredExp = 5000 * lvl;
                var exp = list.Experience;
                var embed = new DiscordEmbedBuilder();
                var member = ctx.Member;
                embed.AddField("User", $"{ctx.Member.DisplayName}#{member.Discriminator.ToString()}", false)
                .AddField("Level", lvl.ToString(), false)
                .AddField("Experience", $"{exp}/{RequiredExp}", false)
                .WithImageUrl(member.AvatarUrl);
                await ctx.Channel.SendMessageAsync(embed.Build());
                


            }





          

        }
    }
}
