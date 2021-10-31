using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class hello : BaseCommandModule
    {
        [Command("hello")]
        [Description("says hello")]
        [Category("rp")]

        public async Task Hello(CommandContext ctx, [Description("Mention a member to say hello to them")]DiscordMember member  )
        {
            
            await ctx.RespondAsync($"Hello there,{member.Mention} ");
        }
    }
}
