using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class BossRole : BaseCommandModule
    {
        [Command("bossrole")]
        [Description("Use this to get boss role")]
        
        
        
        public async Task Role(CommandContext ctx)
        {
            if (!(ctx.Guild.Id == 875583069678092329)) return;
            var member = ctx.Member;
            var role = ctx.Guild.GetRole(876317727801880647);
            await member.GrantRoleAsync(role);
            await ctx.Channel.SendMessageAsync($"You have been granted role {role.Name}");


        }
    }
}
