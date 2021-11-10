using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Rp
{
    class Divorce : BaseCommandModule
    {
        public class DivorceStatus 
        {

            public ulong Id { get; set; }
            public ulong Partner { get; set; }

        }

        [Command("divorce")]
        [Description("divorce your partner")]
        [Category("rp")]
        public async Task DivorceCommand(CommandContext ctx)
        {
            var id = ctx.Member.Id;
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<DivorceStatus>("Marry");
            var filter = Builders<DivorceStatus>.Filter.Eq("_id", id);
            var list =  await collection.Find(filter).FirstOrDefaultAsync();
           
            if(list == null)
            {
                await ctx.RespondAsync("You arent married");
                return;
            }
            else
            {
                var PartnerId = list.Partner;
                var PartnerFilter = Builders<DivorceStatus>.Filter.Eq("_id", PartnerId);
                var FindPartner = collection.Find(PartnerFilter).FirstOrDefault();
                await collection.DeleteOneAsync(filter);
                await collection.DeleteOneAsync(PartnerFilter);
                var user =  await ctx.Guild.GetMemberAsync(PartnerId);
                await ctx.RespondAsync($" {ctx.Member.Mention} Divorces {user.Mention}");
            }
        }
    }
}
