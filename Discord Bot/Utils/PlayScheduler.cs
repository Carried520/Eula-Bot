using DSharpPlus.Lavalink;
using System.Collections.Generic;

namespace Discord_Bot.Utils
{
    class PlayScheduler
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = new Dictionary<ulong, List<LavalinkTrack>>();
    }
}
