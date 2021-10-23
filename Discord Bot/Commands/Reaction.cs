using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Reaction : BaseCommandModule
    {
        [Command("react")]
        [Description("react with emoji")]
        
        public async Task React (CommandContext ctx)
        {
            ctx.Client.UseInteractivity();


            var emoji = DiscordEmoji.FromName(ctx.Client, ":peepoeveryoneleave:", true);
            var message = await ctx.RespondAsync($"{ctx.Member.DisplayName}, react with {emoji}.");

            var result = await message.WaitForReactionAsync(ctx.Member, emoji);

            if (!result.TimedOut) await ctx.Channel.SendMessageAsync("Thank you!");
        }
        
    }
}
