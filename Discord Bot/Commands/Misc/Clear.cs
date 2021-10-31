using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Clear : BaseCommandModule
    {
        [Command("clear")]
        [Description("Clear messages")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        [Category("misc")]
        public async Task ClearCommand (CommandContext ctx, [Description("Number of messages to be deleted")] int quantity)
        {
            if (quantity <= 100)
            {
                await ctx.Channel.DeleteMessagesAsync(await ctx.Channel.GetMessagesAsync(quantity));
            }
            else
            {
                int clears = quantity / 100;
                int remaining = quantity % 100;
                int i = 0;
                while (i < clears + remaining)
                {
                    await ctx.Channel.DeleteMessagesAsync(await ctx.Channel.GetMessagesAsync(clears + remaining));
                    i++;
                }
            }
        }
    }
}
