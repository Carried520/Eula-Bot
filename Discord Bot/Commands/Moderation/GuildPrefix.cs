using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Moderation
{
    class PrefixCommands : BaseCommandModule
    {

        public class GuildPrefix
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }

        }

        public class UserPrefix
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }

        }


        [Command("setprefix")]
        [Category("moderation")]
        [Description("Prefix for you only")]
        public async Task SetPrefix (CommandContext ctx,string Prefix)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<UserPrefix>("userprefixes");
            var Userfilter = Builders<UserPrefix>.Filter.Eq("_id", ctx.Member.Id);
            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            if(UserMatched == null)
            {
                await UserCollection.InsertOneAsync(new UserPrefix { Id = ctx.Member.Id , Prefix = Prefix});
                await ctx.RespondAsync($"Prefix set to {Prefix}");
            }
            else
            {
                var updated = Builders<UserPrefix>.Update.Set("Prefix", Prefix);
                await UserCollection.UpdateOneAsync(Userfilter,updated);
                await ctx.RespondAsync($"Prefix changed to {Prefix}");
            }
        }




        [Command("guildprefix")]
        [Category("moderation")]
        [Description("Prefix for guild")]
        [RequireUserPermissions(DSharpPlus.Permissions.ManageGuild)]
        public async Task SetGuildPrefix(CommandContext ctx, string Prefix)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<GuildPrefix>("guildprefixes");
            var Userfilter = Builders<GuildPrefix>.Filter.Eq("_id", ctx.Guild.Id);
            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            if (UserMatched == null)
            {
                await UserCollection.InsertOneAsync(new GuildPrefix { Id = ctx.Guild.Id, Prefix = Prefix });
                await ctx.RespondAsync($"Prefix set to {Prefix}");
            }
            else
            {
                var updated = Builders<GuildPrefix>.Update.Set("Prefix", Prefix);
                await UserCollection.UpdateOneAsync(Userfilter, updated);
                await ctx.RespondAsync($"Prefix changed to {Prefix}");
            }
        }
    }
}
