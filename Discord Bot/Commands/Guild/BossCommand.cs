using Discord_Bot.Utils;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class BossCommand : BaseCommandModule
    {
        [Command("boss")]
        [Description("Get boss info")]
        public async Task Boss (CommandContext ctx)
        {
            var dict = new BossTimer().getBoss();
            foreach(var key in dict.Keys)
            {
                foreach(var mlem in dict[key])
                {
                    await ctx.RespondAsync($" {key} {mlem.Hour}:{mlem.Minute}");
                }
                
            }
        }
    }
}
