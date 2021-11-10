using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class ResetOne : BaseCommandModule
    {
        public class GuildData 
        {
            public ObjectId _id { get; set; }
            public string Family { get; set; }
            public double Points { get; set; }
            public double Tier { get; set; }

        }

        [Command("resetone")]
        [Description("reset one document")]
        [Category("guild")]
        [RequireCertainGuild(875583069678092329UL)]


        public async Task Reset(CommandContext ctx, string Family)
        {
            
            bool isOfficer = ctx.Member.Roles.Any(x => x.Id == 875592076002230323UL);
            if (!isOfficer || !(ctx.Channel.Id == 875583602610552833UL)) return;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("myFirstDatabase");
            var collection = database.GetCollection<GuildData>("guild-payouts");
            var filter = Builders<GuildData>.Filter.Eq("Family", Family);
             await collection.DeleteManyAsync(filter);
            await ctx.Channel.SendMessageAsync($"Wiped {Family} user");

        }
    }
}
