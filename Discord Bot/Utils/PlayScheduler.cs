using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Utils
{
    class PlayScheduler
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = new Dictionary<ulong, List<LavalinkTrack>>();
    }
}
