using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class BossRole : BaseCommandModule
    {
        [Command("bossrole")]
        [Description("Use this to get boss role")]
        [Category("guild")]
        [RequireCertainGuild(875583069678092329UL)]



        public async Task Role(CommandContext ctx)
        {
            
            var member = ctx.Member;
            var role = ctx.Guild.GetRole(876317727801880647UL);
            await member.GrantRoleAsync(role);
            await ctx.Channel.SendMessageAsync($"You have been granted role {role.Name}");


        }
    }
}
