using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class CoinFlip : BaseCommandModule
    {
        public Random Rng { private get; set; }

        [Command("flip")]
        [Description("Flips a coin")]
        [Category("rp")]
        public async Task Coin(CommandContext ctx, [Description("Heads or Tails")]string input)
        {
            
            string outcome = Rng.Next(0, 1) == 1 ? "heads" : "tails";
           

            if (input.ToLower() == outcome) await ctx.RespondAsync($"You rolled {outcome} and won");
            else await ctx.RespondAsync($"You rolled {outcome} and lost");



        }
    }
}
