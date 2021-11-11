using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;


namespace Discord_Bot.Commands.Guild
{
    class ResetAll : BaseCommandModule
    {
        public class GuildData
        {
            public ObjectId _id { get; set; }
            public string Family { get; set; }
            public double Points { get; set; }
            public double Tier { get; set; }

        }

        [Command("resetall")]
        [Description("reset every document")]
        [Category("guild")]
        [RequireRoleId(875592076002230323UL, 875583069678092329UL)]

        public async Task Reset(CommandContext ctx)
        {
            
            bool isOfficer = ctx.Member.Roles.Any(x => x.Id == 875592076002230323UL);
            if (!isOfficer || !(ctx.Channel.Id == 875583602610552833UL)) return;
            var client = new MongoClient(Config.Get("second_uri"));
            var database = client.GetDatabase("myFirstDatabase");
            var collection = database.GetCollection<GuildData>("guild-payouts");
            await collection.DeleteManyAsync(new BsonDocument());
            await ctx.Channel.SendMessageAsync("Wiped everything if u fucked up I blame you");
            
        }

    }
}
