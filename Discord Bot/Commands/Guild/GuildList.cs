﻿using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class GuildList : BaseCommandModule
    {
        public class GuildData
        {
            public ObjectId _id { get; set; }
            public string Family { get; set; }
            public double Points { get; set; }
            public double Tier { get; set; }

        }

        [Command("guildlist")]
        [Description("list of guild members")]
        [Category("guild")]
        [RequireRoleId(875592076002230323UL, 875583069678092329UL)]

        public async Task ListCommand(CommandContext ctx)
        {
            
            bool isOfficer = ctx.Member.Roles.Any(x => x.Id == 875592076002230323UL);
            if (!(ctx.Channel.Id == 875583602610552833UL)) return;
            var client = new MongoClient(Config.Get("second_uri"));
            var database = client.GetDatabase("myFirstDatabase");
            var collection = database.GetCollection<GuildData>("guild-payouts");
            var documents = await collection.FindAsync(new BsonDocument());
            var ListOfDocuments =  await documents.ToListAsync();
           
            var Family = new StringBuilder();
            var Points = new StringBuilder();
            var Tier = new StringBuilder();            
            foreach(var document in ListOfDocuments)
            {
                Family.Append($"{document.Family}\n");
                Points.Append($"{document.Points}\n");
                Tier.Append($"{document.Tier}\n");
            }

            var embed = new DiscordEmbedBuilder()
               .AddField("Family", Family.ToString(), true)
                .AddField("Points", Points.ToString(), true)
                .AddField("Tier", Tier.ToString(), true)
                .WithColor(DiscordColor.SpringGreen);
            await ctx.Channel.SendMessageAsync(embed.Build());
        }
    }
}
