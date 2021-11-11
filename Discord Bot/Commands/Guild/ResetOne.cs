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
        [RequireRoleId(875592076002230323UL, 875583069678092329UL)]


        public async Task Reset(CommandContext ctx, string Family)
        {
            
           
            if (!(ctx.Channel.Id == 875583602610552833UL)) return;
            var client = new MongoClient(Config.Get("second_uri"));
            var database = client.GetDatabase("myFirstDatabase");
            var collection = database.GetCollection<GuildData>("guild-payouts");
            var filter = Builders<GuildData>.Filter.Eq("Family", Family);
             await collection.DeleteManyAsync(filter);
            await ctx.Channel.SendMessageAsync($"Wiped {Family} user");

        }
    }
}
