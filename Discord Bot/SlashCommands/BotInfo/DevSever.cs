using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class DevSever : ApplicationCommandModule
    {
        [SlashCommand("botserver","CarriedValk's discord server")]
        public async Task BotServer(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var ServerInfo= await ctx.Client.GetGuildAsync(878330102763646976L);
            var embed = new DiscordEmbedBuilder()
                .WithDescription("Get Bot Dev Server invite")
                .WithImageUrl(ServerInfo.IconUrl)
                .WithUrl("https://discord.gg/bTT6Rcdcgr");
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed.Build()));
        }
    }
}
