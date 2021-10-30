using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class Botinvite : ApplicationCommandModule
    {
        [SlashCommand("botinvite","get bot invite link")]
        public async Task Invite(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(
                "https://discord.com/oauth2/authorize?client_id=713127586976366604&permissions=0&scope=bot%20applications.commands"));
        }
    }
}
