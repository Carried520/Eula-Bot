using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
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
        [Description("divorce this bish")]
        public async Task DivorceCommand(CommandContext ctx)
        {
            var id = ctx.Member.Id;
            var client = new MongoClient(Config.get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<DivorceStatus>("Marry");
            var filter = Builders<DivorceStatus>.Filter.Eq("_id", id);
            var list = collection.Find(filter).FirstOrDefaultAsync().Result;
           
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
            }
        }
    }
}
