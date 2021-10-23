using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Unmute : BaseCommandModule
    {
        [Command("unmute")]
        [Description("unmute members")]
        [RequireUserPermissions(DSharpPlus.Permissions.MuteMembers)]

        public async Task UnmuteCommand(CommandContext ctx, DiscordMember member)
        {

            await member.SetMuteAsync(false);
            await ctx.Channel.SendMessageAsync($"Unmuted {member.Mention}");

        }
    }
}
