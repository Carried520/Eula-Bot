using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.BotInfo
{
    class DevServerLink : BaseCommandModule
    {
        [Command("botserver")]
        [Description("Get Bot Dev Server invite link")]
        [Category("botinfo")]
        public async Task ServerLink (CommandContext ctx)
        {
            var ServerInfo =  await ctx.Client.GetGuildAsync(878330102763646976L);
            var embed = new DiscordEmbedBuilder()
                .WithDescription("Get Bot Dev Server invite")
                .WithImageUrl(ServerInfo.IconUrl)
                .WithUrl("https://discord.gg/bTT6Rcdcgr");
            await ctx.Channel.SendMessageAsync(embed);  
        }
    }
}
