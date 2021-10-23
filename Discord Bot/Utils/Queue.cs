using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Utils
{
    class ListSongs
    {
        public List<LavalinkTrack> queue { get; set; } = new List<LavalinkTrack>();
        
        public void Add (LavalinkTrack track){

            queue.Add(track);
        }
        public void Remove()
        {
            queue.RemoveAt(0);
        }
        public LavalinkTrack Get()
        {
           return queue[0];
        }

    }
}
