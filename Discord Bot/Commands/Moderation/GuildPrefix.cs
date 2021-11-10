using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.BotInfo
{
    class PrefixCommands : BaseCommandModule
    {

        public class GuildPrefix
        {
            public ulong Id { get; set; }
            public List<string> Prefixes { get; set; }

        }


        public class UserPrefix
        {
            public ulong Id { get; set; }
            public List<string> Prefixes { get; set; }

        }

        [Command("setprefix")]
        [Category("moderation")]
        [Description("Prefix for you only")]
        public async Task SetPrefix (CommandContext ctx,string Prefix)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                await ctx.RespondAsync("Prefix cant be empty");
                return;
            }
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<UserPrefix>("userprefixes");
            var Userfilter = Builders<UserPrefix>.Filter.Eq("_id", ctx.Member.Id);
            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            var ListOfPrefixes = new List<string>{
                Prefix
            };
            if(UserMatched == null)
            {
                await UserCollection.InsertOneAsync(new UserPrefix { Id = ctx.Member.Id , Prefixes = ListOfPrefixes});
                await ctx.RespondAsync($"Prefix set to {Prefix}");
            }
            else
            {
                var PreviousList = UserMatched.Prefixes;
                PreviousList.Add(Prefix);
                if (PreviousList.Count > 10)
                {
                    await ctx.RespondAsync("You can only have 10  user prefixes");
                }
                var updated = Builders<UserPrefix>.Update.Set("Prefixes", PreviousList);
                await UserCollection.UpdateOneAsync(Userfilter,updated);
                await ctx.RespondAsync($"Prefix added  {Prefix}");
            }
        }




        [Command("guildprefix")]
        [Category("moderation")]
        [Description("Prefix for guild")]
        [RequireUserPermissions(DSharpPlus.Permissions.ManageGuild)]
        public async Task SetGuildPrefix(CommandContext ctx, string Prefix)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                await ctx.RespondAsync("Prefix cant be empty");
                return;
            }
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<GuildPrefix>("guildprefixes");
            var Userfilter = Builders<GuildPrefix>.Filter.Eq("_id", ctx.Guild.Id);
            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            var ListOfPrefixes = new List<string>{
                Prefix
            };
            if (UserMatched == null)
            {
                await UserCollection.InsertOneAsync(new GuildPrefix { Id = ctx.Guild.Id, Prefixes = ListOfPrefixes });
                await ctx.RespondAsync($"Prefix set to {Prefix}");
            }
            else
            {
                var PreviousList = UserMatched.Prefixes;
                PreviousList.Add(Prefix);
                if (PreviousList.Count > 10)
                {
                    await ctx.RespondAsync("You can only have 10  guild prefixes");
                }
                var updated = Builders<GuildPrefix>.Update.Set("Prefixes", PreviousList);
                await UserCollection.UpdateOneAsync(Userfilter, updated);
                await ctx.RespondAsync($" Guild Prefix added :  {Prefix}");
            }
        }
    }
}
