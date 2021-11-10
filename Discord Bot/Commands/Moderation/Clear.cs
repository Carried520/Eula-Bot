using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Clear : BaseCommandModule
    {
        [Command("clear")]
        [Description("Clear messages")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        [Category("moderation")]
        public async Task ClearCommand (CommandContext ctx, [Description("Number of messages to be deleted")] int quantity)
        {
            if (quantity <2 || quantity >100)
            {
                await ctx.RespondAsync("quantity must be greater than 2 and cant be more than 100");
                return;
            }
            if (quantity <= 100)
            {
                await ctx.Channel.DeleteMessagesAsync(await ctx.Channel.GetMessagesAsync(quantity));
            }
            
        }
    }
}
