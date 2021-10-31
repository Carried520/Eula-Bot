using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using System;
using System.Collections.Generic;
using System.Text;
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
            
          await  member.SetMuteAsync(true);
            await ctx.Channel.SendMessageAsync($"Muted {member.Mention}");

        }
    }
}
