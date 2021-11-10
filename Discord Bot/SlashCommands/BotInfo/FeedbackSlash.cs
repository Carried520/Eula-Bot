using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class FeedbackSlash : ApplicationCommandModule
    {
        [SlashCommand("feedback","send feedback")]
        
        public async Task FeedbackCommand(InteractionContext ctx,[Option("feedback","feedback to send")]string feedback)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("feedback sent"));
            await ctx.Client.GetGuildAsync(878330102763646976L).Result.GetChannel(903764027627438081L).SendMessageAsync(feedback);
        }
    }
}
