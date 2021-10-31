using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Clock : BaseCommandModule
    {
        [Command("timer")]
        [Description("set an alert")]
        [Category("misc")]
        public async Task Time(CommandContext ctx,int minutes)
        {
            var timespan = TimeSpan.FromMinutes(minutes);
            await Task.Delay(timespan);
            var member = ctx.Member;
            await ctx.RespondAsync($"{member.Mention} time has passed");
        }
    }
}
