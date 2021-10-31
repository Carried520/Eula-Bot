using Discord_Bot.Attributes;
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
    class Track : BaseCommandModule
    {
        [Command("track")]
        [Description("gets current track")]
        [Category("music")]
        public async Task TrackCommand(CommandContext ctx)
        {


            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }


            await ctx.Channel.SendMessageAsync($"Current track : {conn.CurrentState.CurrentTrack.Title} at " +
                $"{(Convert.ToInt32(conn.CurrentState.PlaybackPosition.TotalSeconds/60))} minutes {Convert.ToInt32(conn.CurrentState.PlaybackPosition.TotalSeconds  % 60)} seconds");


        }
    }
}
