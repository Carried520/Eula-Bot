using Discord_Bot.Attributes;
using Discord_Bot.Utils;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class Shuffle : BaseCommandModule
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        [Command("shuffle")]
        [Description("Shuffle queue")]
        [Category("music")]
        public async Task Reorder(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            if (!music.ContainsKey(ctx.Guild.Id))
            {
                await ctx.RespondAsync("No queue running at the moment");
                return;
            }
            var newQueue = music[ctx.Guild.Id];
            newQueue.Shuffle();
            music[ctx.Guild.Id] = newQueue;
            await ctx.Channel.SendMessageAsync("Shuffled queue");
        }
    }
}
