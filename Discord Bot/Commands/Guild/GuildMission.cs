using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class GuildMission : BaseCommandModule
    {

        public class GuildData
        {
            public ObjectId _id { get; set; }
            public string Family { get; set; }
            public double Points { get; set; }
            public double Tier { get; set; }

        }
        
        [Command("guildmission")]
        [Description("Guildmission tool")]
        [Category("guild")]

        public async Task  GuildCommand(CommandContext ctx,params string[] content)
        {
            if (!(ctx.Guild.Id == 875583069678092329)) return;
            bool isOfficer = ctx.Member.Roles.Any(x => x.Id == 875592076002230323);
            if (!isOfficer || !(ctx.Channel.Id == 875583602610552833)) return;

            var list = content.ToList();
            list.RemoveAll(x=>x==",");
            int sign = list.IndexOf("-");
            var ListOfGuildMember = new List<string>();
            for(int i = sign + 1; i < list.Count; i++)
            {
                ListOfGuildMember.Add(list[i]);
            }

            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("myFirstDatabase");
            var collection = database.GetCollection<GuildData>("guild-payouts");
            var builder = new StringBuilder();
            
            foreach (string member in ListOfGuildMember)
            {
               
                var filter = Builders<GuildData>.Filter.Eq("Family", member);
                var result = collection.Find(filter).FirstOrDefaultAsync().Result;
                if(result == null)
                {
                    double Points = 0;
                    double Tier = 0;
                    if (list.Contains("smh"))
                    {
                        Points = 0.5;
                        Tier = 0.5;
                        
                        
                    }
                    else if (list.Contains("boss"))
                    {
                        Points = 2.0;
                        Tier = 2.0;
                        
                    }

                    else
                    {
                        Points = 1.0;
                        Tier = 1.0;
                        


                    }
                    builder.Append($"```{member} 0->{Tier}```").Append("\n");
                    await collection.InsertOneAsync(new GuildData { Family = member , Points = Points , Tier = Tier});

                }
                else
                {
                    double OldPoints = result.Points;
                    double OldTier = result.Tier;
                    double NewPoints = 0;
                    double NewTier = 0;
                    if (list.Contains("smh"))
                    {
                        NewPoints = OldPoints + 0.5;
                        NewTier = OldTier + 0.5;
                        
                    }
                    else if (list.Contains("boss"))
                    {
                        NewPoints = OldPoints + 2.0;
                        NewTier = OldTier + 2.0;
                        
                    }

                    else
                    {
                    
                        NewPoints = OldPoints + 1.0;
                        NewTier = OldTier + 1.0;
                    }
                    builder.Append($"```{member} {OldTier}->{NewTier}```").Append("\n");
                    var updated = Builders<GuildData>.Update.Set("Points", NewPoints).Set("Tier", NewTier);
                    collection.UpdateOne(filter, updated);

                }
            }
            await ctx.Channel.SendMessageAsync(builder.ToString());
            
           
        }
    }
}
