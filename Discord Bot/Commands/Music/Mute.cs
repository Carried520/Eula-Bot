using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Mute : BaseCommandModule
    {
        [Command("mute")]
        [Description("mute members")]
        [Category("music")]
        [RequireUserPermissions(DSharpPlus.Permissions.MuteMembers)]

        public  async Task MuteCommand(CommandContext ctx,DiscordMember member)
        {
            if (member.VoiceState == null || member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            await  member.SetMuteAsync(true);
            await ctx.Channel.SendMessageAsync($"Muted {member.Mention}");

        }
    }
}
