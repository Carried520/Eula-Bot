using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{

    public class getJSON
    {
        public string image { get; set; }
        public string link { get; set; }

    }



    public class Fox : BaseCommandModule
    {

        [Command("fox")]
        [Description("Random fox image")]
        [Category("misc")]
        public async Task FoxCommand(CommandContext ctx)
        {

            var webClient = new WebClient().DownloadString("https://randomfox.ca/floof/");
            var response = JsonSerializer.Deserialize<getJSON>(webClient);
            var embed = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Cyan)
                .WithImageUrl(response.image)
                .WithTimestamp(DateTime.Now)
                .WithFooter("Powered by C#")
                .WithTitle("Random Fox")
                .Build();
            await ctx.Channel.SendMessageAsync(embed);


        }
    }
}
