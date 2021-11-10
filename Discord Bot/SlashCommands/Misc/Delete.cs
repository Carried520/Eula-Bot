using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class Delete : ApplicationCommandModule
    {
        [SlashCommand("delete", "Delete messages")]
        [SlashRequirePermissions(Permissions.ManageMessages)]
        public async Task DeleteCommand(InteractionContext ctx, [Option("amount", "Amount of messages to purge")] long amount) {
            if(amount<2 || amount > 100)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Amount of messages must be greater than 2 and no more than 100"));
                return;
            }
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Cleared {amount} messages"));
            await ctx.Channel.DeleteMessagesAsync(await ctx.Channel.GetMessagesAsync(Convert.ToInt32(amount)));
            
            

        }
    }
}
