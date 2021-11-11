using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Attributes
{
    class RequireRoleId : CheckBaseAttribute
    {
        public ulong RoleId;
        public ulong GuildId;
        public RequireRoleId(ulong roleId,ulong guildId)
        {
            this.RoleId = roleId;
            this.GuildId = guildId;
        }

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            if (ctx.Guild.Id != GuildId) return Task.FromResult(false);
            var Roles = ctx.Member.Roles;
            var SearchedRole = ctx.Guild.GetRole(RoleId);
            return Task.FromResult(Roles.Contains(SearchedRole));
        }
    }
}
