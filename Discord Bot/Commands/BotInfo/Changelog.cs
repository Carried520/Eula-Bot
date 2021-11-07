using Discord_Bot.Attributes;
using Discord_Bot.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.BotInfo
{
    class Changelog : BaseCommandModule
    {
       
        [Command("changelog")]
        [Category("botinfo")]
        public async Task Change (CommandContext ctx)
        {
            var builder = new StringBuilder();
            foreach(var line in File.ReadLines(@"Changelog.txt"))
            {
                builder.Append(line).Append("\n");
            }
            var embed = new DiscordEmbedBuilder()
                .AddField("Eula Bot Changelog", builder.ToString(),false);
            await ctx.RespondAsync(embed.Build());
        }
    }
}
