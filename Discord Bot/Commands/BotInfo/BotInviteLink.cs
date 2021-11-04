using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.BotInfo
{
    class BotInviteLink : BaseCommandModule
    {
        [Command("botinvite")]
        [Description("get bot invite link")]
        [Category("botinfo")]
        public async Task Invite(CommandContext ctx)
        {
            var button = new DiscordLinkButtonComponent("https://discord.com/oauth2/authorize?client_id=713127586976366604&permissions=0&scope=bot%20applications.commands", "Invite Bot");
            var builder = new DiscordMessageBuilder()
                .WithContent("Click button below")
                .AddComponents(button);
            await ctx.Channel.SendMessageAsync(builder);
        }
    }
}
