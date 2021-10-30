using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.BotInfo
{
    class FeedbackCommand : BaseCommandModule
    {
        [Command("feedback")]
        [Description("feedback about bot")]
        public async Task Feedback (CommandContext ctx,string feedback)
        {
            await ctx.RespondAsync("Feedback sent");
            await ctx.Client.GetGuildAsync(878330102763646976L).Result.GetChannel(903764027627438081L).SendMessageAsync(feedback);
        }
    }
}
