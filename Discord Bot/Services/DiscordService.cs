using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Services
{
   public class DiscordService
    {

        public async Task SendMessage(CommandContext ctx , string message)
        {
            var channel = ctx.Channel;
            await channel.SendMessageAsync(message);
        }
        public async Task SendRpEmbed(CommandContext ctx, string url, string property, string thing, DiscordMember member)
        {
            var webclient = new WebClient().DownloadString(url);
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty(property);
            

            var embed = new DiscordEmbedBuilder()
                .WithDescription($"{ctx.Member.Mention} {thing} {member.Mention} ")
                .WithImageUrl(results.GetString())
                .Build();
            await ctx.RespondAsync(embed);
        }











    }
}
