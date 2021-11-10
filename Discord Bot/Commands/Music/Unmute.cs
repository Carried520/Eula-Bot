using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Unmute : BaseCommandModule
    {
        [Command("unmute")]
        [Description("unmute members")]
        [RequireUserPermissions(DSharpPlus.Permissions.MuteMembers)]
        [Category("music")]

        public async Task UnmuteCommand(CommandContext ctx, DiscordMember member)
        {
            if (member.VoiceState == null || member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            await member.SetMuteAsync(false);
            await ctx.Channel.SendMessageAsync($"Unmuted {member.Mention}");

        }
    }
}
