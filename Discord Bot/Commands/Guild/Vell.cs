using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Guild
{
    class Vell : BaseCommandModule
    {
        [Command("vell")]
        [Description("Announce vell")]
        [Category("guild")]
        [RequireRoleId(875592076002230323UL, 875583069678092329UL)]

        public async Task Announce (CommandContext ctx, int hour , int minute)
        {
            var guild = await ctx.Client.GetGuildAsync(875583069678092329UL);
            var channel =  guild.GetChannel(875584695956566026UL);
            var role = guild.GetRole(875592174065049630UL);
            var content = $"{role.Mention} Ding ding Time for Vell Type '+' in guild chat for invite velia MEDIAH 6 to platoon 1 pls bring a compass and a map. Leaving Velia" +
                $" {hour}:{minute} CET.This vell plat will be led by {ctx.Member.DisplayName}";
            await channel.SendMessageAsync(content);
        }
    }
}
