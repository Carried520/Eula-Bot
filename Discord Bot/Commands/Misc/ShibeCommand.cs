using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class ShibeCommand : BaseCommandModule
    {
        [Command("shibe")]
        [Description("random shibe")]
        [Category("misc")]
        public async Task Shibe(CommandContext ctx)
        {
            string url = "http://shibe.online/api/shibes?count=1&urls=true&httpsUrls=true";
            string webClient = new WebClient().DownloadString(url);
            JsonDocument json = JsonDocument.Parse(webClient);
            JsonElement root = json.RootElement;
            var results = root[0];
            var embed = new DiscordEmbedBuilder()
               .WithColor(DiscordColor.SpringGreen)
               .WithImageUrl(results.GetString())
               .WithTimestamp(DateTime.Now)
               .WithFooter("Powered by C#(not this shitty Java)")
               .WithTitle("Random Shibe")
               .Build();
            await ctx.Channel.SendMessageAsync(embed);


        }
    }
}
