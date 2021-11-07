using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Net;
using System.Text.Json;

namespace Discord_Bot.Services
{
    class SlashService
    {
        public DiscordEmbed SendRpEmbed(InteractionContext ctx,string url,string property,string thing,DiscordUser user)
        {
            var webclient = new WebClient().DownloadString(url);
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty(property);
            var member = (DiscordMember)user;

            var embed = new DiscordEmbedBuilder()
                .WithDescription($"{ctx.Member.Mention} {thing} {member.Mention} ")
                .WithImageUrl(results.GetString())
                .Build();
            return embed;
        }
    }
}
