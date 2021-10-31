using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Volume : BaseCommandModule
    {
        [Command("volume")]
        [Description("Set bot volume")]
        [Category("music")]

        public async Task VolumeCommand(CommandContext ctx, int volume)
        {


            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }




            await conn.SetVolumeAsync(volume);

        }
    }
}
