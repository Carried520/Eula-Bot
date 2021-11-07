using Discord_Bot.Attributes;
using Discord_Bot.Utils;
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
    class Stop : BaseCommandModule
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        [Command("stop")]
        [Description("Clear the queue and stop the player")]
        [Category("music")]
        public async Task StopCommand(CommandContext ctx)
        {

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }
            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("Player isnt playing now");
            }
            if (!music.ContainsKey(ctx.Guild.Id)) return;
            var newQueue = music[ctx.Guild.Id];
            newQueue.Clear();
            music[ctx.Guild.Id] = newQueue;
            await conn.StopAsync();
            
            
            await ctx.Channel.SendMessageAsync("Cleared queue and stopped the player");
        }
    }
}
