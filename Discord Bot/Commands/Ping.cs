using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Ping :BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns bot ping")]
        public async Task PingCommand(CommandContext ctx)
        {

            await ctx.RespondAsync($"Bot ping: {ctx.Client.Ping}ms");

        }
    }
}
