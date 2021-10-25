using Discord_Bot.Utils;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class Shuffle : BaseCommandModule
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        [Command("shuffle")]
        [Description("Shuffle queue")]
        public async Task Reorder(CommandContext ctx)
        {
            var newQueue = music[ctx.Guild.Id];
            newQueue.Shuffle();
            music[ctx.Guild.Id] = newQueue;
            await ctx.Channel.SendMessageAsync("Shuffled queue");
        }
    }
}
