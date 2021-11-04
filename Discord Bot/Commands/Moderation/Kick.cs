using Discord_Bot.Attributes;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Moderation
{
    class Kick : BaseCommandModule
    {
        [Command("kick")]
        [Description("kick from vc")]
        [Category("moderation")]
        [RequirePermissions(Permissions.MoveMembers)]
        public async Task KickCommand(CommandContext ctx, DiscordMember member)
        {
            if (member.VoiceState == null || member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync($"{member.Nickname} is not connected to voice channel dumbo");
                return;
            }
            await member.ModifyAsync(member => member.VoiceChannel = null);
            await ctx.RespondAsync($"Disconnected {member.Nickname} from voice channel");
            
          
        }
    }
}
