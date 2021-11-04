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
    class MoveToAfk : BaseCommandModule
    {
        [Command("afk")]
        [Description("move to vc")]
        [Category("moderation")]
        [RequirePermissions(Permissions.MoveMembers)]
        public async Task MoveAfkMember(CommandContext ctx, DiscordMember member)
        {
            if (member.VoiceState == null || member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync($"{member.Nickname} is not connected to voice channel dumbo");
                return;
            }
            var guildAfk = ctx.Guild.AfkChannel;
            if(guildAfk == null)
            {
                await ctx.RespondAsync("Server doesnt have afk channel");
            }
            await member.PlaceInAsync(guildAfk);
            await ctx.RespondAsync($"Moved {member.Nickname} to afk channel");


        }
    }
}
