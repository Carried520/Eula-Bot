using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class RandomCommand : BaseCommandModule
    {
        public Random Rng { private get; set; }




        [Command("random")]
        [Description("Generate Random number")]
        [Category("misc")]



        public async Task Rand(CommandContext ctx, [Description("Min number")] int min, [Description("Max number")] int max)
        {
            if(min == max)
            {
                await ctx.RespondAsync("Min number cannot be same as Max number");
                return;
            }
      
            await ctx.RespondAsync($"Your number is:{Rng.Next(min, max)}");

        }
    }
}
