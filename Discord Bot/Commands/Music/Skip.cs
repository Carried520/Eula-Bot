using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class Skip : BaseCommandModule
    {
        [Command("skip")]
        [Description("skips a track")]
        public async Task StopCommand (CommandContext ctx)
        {

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }
           if(conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("Player isnt playing now");
            }
           await conn.StopAsync();
           
           
        }
    }
}
