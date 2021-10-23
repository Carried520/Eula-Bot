using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Dm : BaseCommandModule
    {
        [Command("dm")]
        [Description("dm someone")]

        public async Task DmCommand(CommandContext ctx , DiscordMember member,[RemainingText] string  content)
        {
            
            
            await member.CreateDmChannelAsync().Result.SendMessageAsync(content);
            await ctx.Channel.DeleteMessageAsync(ctx.Message);
        }
    }
}
