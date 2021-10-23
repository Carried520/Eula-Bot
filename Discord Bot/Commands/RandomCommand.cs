using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class RandomCommand : BaseCommandModule
    {
        public Random Rng { private get; set; }




        [Command("random")]
        [Description("Generate Random number")]
       


        public async Task Rand(CommandContext ctx, [Description("Min number")] int min, [Description("Max number")] int max)
        {
      
            await ctx.RespondAsync($"Your number is:{Rng.Next(min, max)}");

        }
    }
}
